using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Child.ParentsAndChildren;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.DataBase.Entity.Contract;
using WpfDormitories.DataBase.Entity.District;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.Inventory;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;

namespace WpfDormitories.Model.Services.Tables
{
    public class RoomsTableService : ITableService
    {
        private uint _dormId;
        private List<IRoomData> _rooms;
        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }

        public RoomsTableService(uint dormId)
        {
            _dormId = dormId;
        }
        public void Edit(int index)
        {
            OnEdit.Invoke(GetByIndex(index));
        }
        public DataTable FindAll(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }

            _rooms = DataManager.GetInstance().RoomsRepository.Read().ToList();

            DataTable res = CreateDataTable();

            List<IRoomData> resRooms = new();

            foreach (IRoomData room in _rooms)
            {
                if (room.NumberRoom.Contains(text))
                {
                    resRooms.Add(room);
                }
            }
            _rooms = resRooms;

            foreach (IRoomData room in _rooms)
            {
                res.Rows.Add(room.NumberRoom, room.RoomArea, room.Floor, room.TotalNumberPlaces, room.NumberFreePlaces);
            }
            return res;
        }

        public DataTable Read()
        {
            List<IRoomData> allRooms = DataManager.GetInstance().RoomsRepository.Read().ToList();
            _rooms = new();
            foreach (IRoomData room in allRooms)
            {
                if(room.DormId == _dormId)
                {
                    _rooms.Add(room);
                }
            }
            DataTable res = CreateDataTable();
            foreach (IRoomData room in _rooms)
            {
                res.Rows.Add(room.NumberRoom, room.RoomArea, room.Floor, room.TotalNumberPlaces, room.NumberFreePlaces);
            }
            return res;
        }

        private DataTable CreateDataTable()
        {
            DataTable res = new();
            res.Columns.Add("Номер");
            res.Columns.Add("Площадь");
            res.Columns.Add("Этаж");
            res.Columns.Add("Общее число мест");
            res.Columns.Add("Число свободных мест");
            return res;
        }

        public void Add()
        {
            OnAdd.Invoke();
        }

        public void Delete(int index)
        {
            List<IInventoryData> inventory = DataManager.GetInstance().InventoryRepository.Read().ToList();
            List<IResidentData> residents = DataManager.GetInstance().ResidentsRepository.Read().ToList();
            List<IChildData> children = DataManager.GetInstance().ChildrenRepository.Read().ToList();
            List<IParentsAndChildrenData> parentsAndChildren = DataManager.GetInstance().ParentsAndChildrenRepository.Read().ToList();
            List<IContractData> contracts = DataManager.GetInstance().ContractsRepository.Read().ToList();

            List<IInventoryData> inventoryInRoom = inventory.FindAll(item => item.RoomId == _rooms[index].Id);
            List<IResidentData> residentsInRoom = residents.FindAll(item => item.RoomId == _rooms[index].Id);

            foreach (IInventoryData item in inventoryInRoom)
            {
                DataManager.GetInstance().InventoryRepository.Delete(item);
                inventoryInRoom.Remove(item);
            }
            foreach (IResidentData resident in residentsInRoom)
            {
                List<IParentsAndChildrenData> parentsAndChildrenByParent = parentsAndChildren.FindAll(item => item.ParentId == resident.Id);
                foreach (IParentsAndChildrenData parentAndChild in parentsAndChildrenByParent)
                {
                    IChildData child = children.Find(item => item.Id == parentAndChild.ChildId);
                    if (child != null)
                    {
                        DataManager.GetInstance().ChildrenRepository.Delete(child);
                        children.Remove(child);
                    }

                    DataManager.GetInstance().ParentsAndChildrenRepository.Delete(parentAndChild);
                    parentsAndChildren.Remove(parentAndChild);
                }
                IContractData contract = contracts.Find(item => item.Id == resident.ContractId);
                if (contract != null)
                {
                    DataManager.GetInstance().ContractsRepository.Delete(contract);
                    contracts.Remove(contract);
                }

                DataManager.GetInstance().ResidentsRepository.Delete(resident);
                residents.Remove(resident);
            }
            
            DataManager.GetInstance().RoomsRepository.Delete(_rooms[index]);
            _rooms.Remove(_rooms[index]);
        }
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable<IRoomData>(_rooms).Rows[index];
        }
    }
}
