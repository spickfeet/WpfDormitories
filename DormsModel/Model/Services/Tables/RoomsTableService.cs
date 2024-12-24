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
        public RoomsTableService()
        {
        }

        /// <summary>
        /// Вызвать событие для изменения объекта.
        /// </summary>
        /// <param name="index"></param>
        public void Edit(int index)
        {
            OnEdit.Invoke(GetByIndex(index));
        }

        /// <summary>
        /// Найти объекты по тексту.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public DataTable FindAll(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }

            List<IRoomData> allRooms = DataManager.GetInstance().RoomsRepository.Read().ToList();
            _rooms = new();
            foreach (IRoomData room in allRooms)
            {
                if (room.DormId == _dormId || _dormId == 0)
                {
                    _rooms.Add(room);
                }
            }

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

        /// <summary>
        /// Прочитать все объекты.
        /// </summary>
        /// <returns></returns>
        public virtual DataTable Read()
        {
            List<IRoomData> allRooms = DataManager.GetInstance().RoomsRepository.Read().ToList();
            _rooms = new();
            foreach (IRoomData room in allRooms)
            {
                if(room.DormId == _dormId || _dormId == 0)
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

        /// <summary>
        /// Создать таблицу.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Вызвать событие для добавления объекта.
        /// </summary>
        /// <param></param>
        public void Add()
        {
            OnAdd.Invoke();
        }

        /// <summary>
        /// Удалить объект по индексу.
        /// </summary>
        /// <param name="index"></param>
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
            }
            foreach (IResidentData resident in residentsInRoom)
            {
                List<IParentsAndChildrenData> parentsAndChildrenByParent = parentsAndChildren.FindAll(item => item.ParentId == resident.Id);
                foreach (IParentsAndChildrenData parentAndChild in parentsAndChildrenByParent)
                {
                    DataManager.GetInstance().ParentsAndChildrenRepository.Delete(parentAndChild);
                }
                DataManager.GetInstance().ResidentsRepository.Delete(resident);
            }            

            parentsAndChildren = DataManager.GetInstance().ParentsAndChildrenRepository.Read().ToList();
            foreach (IChildData child in children)
            {
                List<IParentsAndChildrenData> parentsAndChildrenByChildren = parentsAndChildren.FindAll(item => item.ChildId == child.Id);
                if (parentsAndChildrenByChildren.Count == 0)
                {
                    DataManager.GetInstance().ChildrenRepository.Delete(child);
                }
            }

            residents = DataManager.GetInstance().ResidentsRepository.Read().ToList();
            foreach (IContractData contract in contracts)
            {
                List<IResidentData> residentsByContract = residents.FindAll(item => item.ContractId == contract.Id);
                if (residentsByContract.Count == 0)
                {
                    DataManager.GetInstance().ContractsRepository.Delete(contract);
                }
            }

            DataManager.GetInstance().RoomsRepository.Delete(_rooms[index]);
            _rooms.Remove(_rooms[index]);
        }

        /// <summary>
        /// Получить объект по индексу.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable<IRoomData>(_rooms).Rows[index];
        }
    }
}
