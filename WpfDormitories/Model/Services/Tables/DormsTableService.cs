using System.Data;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase.Entity.District;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase.Entity.Inventory;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.DataBase.Entity.Child.ParentsAndChildren;
using WpfDormitories.DataBase.Entity.Contract;

namespace WpfDormitories.Model.Services.Tables
{
    public class DormsTableService : ITableService
    {
        private List<IStreetData> _streets;
        private List<IDistrictData> _districts;
        private List<IDormData> _dorms;
        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }

        public DormsTableService()
        {
            _streets = DataManager.GetInstance().StreetsRepository.Read().ToList<IStreetData>();
            _districts = DataManager.GetInstance().DistrictsRepository.Read().ToList<IDistrictData>();
        }
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IDormData>(_dorms).Rows[index]);
        }
        public DataTable FindAll(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }

            _dorms = DataManager.GetInstance().DormsRepository.Read().ToList<IDormData>();

            DataTable res = CreateDataTable();

            List<IDormData> resDorms = new();

            foreach (IDormData dorm in _dorms)
            {
                if (_streets.Find(item => item.Id == dorm.StreetId).Name.ToLower().Contains(text.ToLower()) ||
                        _districts.Find(item => item.Id == dorm.DistrictId).Name.ToLower().Contains(text.ToLower()) ||
                        dorm.DormNumber.Contains(text))
                {
                    resDorms.Add(dorm);
                }
            }
            _dorms = resDorms;

            foreach (IDormData dorm in _dorms)
            {
                res.Rows.Add(_streets.Find(item => item.Id == dorm.StreetId).Name, _districts.Find(item => item.Id == dorm.DistrictId).Name,
                    dorm.DormNumber, dorm.HouseNumber, dorm.NumberRooms, dorm.NumberPlace);
            }
            return res;
        }

        public DataTable Read()
        {
            _dorms = DataManager.GetInstance().DormsRepository.Read().ToList<IDormData>();
            DataTable res = CreateDataTable();
            foreach (IDormData dorm in _dorms)
            {
                res.Rows.Add(_streets.Find(item => item.Id == dorm.StreetId).Name, _districts.Find(item => item.Id == dorm.DistrictId).Name,
                    dorm.DormNumber, dorm.HouseNumber, dorm.NumberRooms, dorm.NumberPlace);
            }
            return res;
        }

        private DataTable CreateDataTable()
        {
            DataTable res = new();
            res.Columns.Add("Улица");
            res.Columns.Add("Район");
            res.Columns.Add("Номер общежития");
            res.Columns.Add("Номер дома");
            res.Columns.Add("Количество комнат");
            res.Columns.Add("Количество мест");
            return res;
        }

        public void Add()
        {
            OnAdd.Invoke();
        }

        public void Delete(int index)
        {

            List<IRoomData> rooms = DataManager.GetInstance().RoomsRepository.Read().ToList().FindAll(item => item.DormId == _dorms[index].Id);
            List<IInventoryData> inventory = DataManager.GetInstance().InventoryRepository.Read().ToList();
            List<IResidentData> residents = DataManager.GetInstance().ResidentsRepository.Read().ToList();
            List<IChildData> children = DataManager.GetInstance().ChildrenRepository.Read().ToList();
            List<IParentsAndChildrenData> parentsAndChildren = DataManager.GetInstance().ParentsAndChildrenRepository.Read().ToList();
            List<IContractData> contracts = DataManager.GetInstance().ContractsRepository.Read().ToList();

            foreach (IRoomData room in rooms)
            {
                List<IInventoryData> inventoryInRoom = inventory.FindAll(item => item.RoomId == room.Id);
                List<IResidentData> residentsInRoom = residents.FindAll(item => item.RoomId == room.Id);

                foreach (IInventoryData item in inventoryInRoom)
                {
                    DataManager.GetInstance().InventoryRepository.Delete(item);
                }
                foreach (IResidentData resident in residentsInRoom)
                {
                    List<IParentsAndChildrenData> parentsAndChildrenByParent = parentsAndChildren.FindAll(item => item.ParentId == resident.Id);
                    foreach(IParentsAndChildrenData parentAndChild in parentsAndChildrenByParent)
                    {
                        DataManager.GetInstance().ParentsAndChildrenRepository.Delete(parentAndChild);
                    }
          
                }
                foreach (IResidentData resident in residentsInRoom)
                {
                    DataManager.GetInstance().ResidentsRepository.Delete(resident);
                }
                DataManager.GetInstance().RoomsRepository.Delete(room);
            }

            parentsAndChildren = DataManager.GetInstance().ParentsAndChildrenRepository.Read().ToList();
            foreach(IChildData child in children)
            {
                List<IParentsAndChildrenData> parentsAndChildrenByChildren = parentsAndChildren.FindAll(item => item.ChildId == child.Id);
                if(parentsAndChildrenByChildren.Count == 0)
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

            DataManager.GetInstance().DormsRepository.Delete(_dorms[index]);
            _dorms.Remove(_dorms[index]);
        }
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable<IDormData>(_dorms).Rows[index];
        }
    }
}
