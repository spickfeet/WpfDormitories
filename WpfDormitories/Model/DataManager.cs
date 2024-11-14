using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.DataBase.Entity.Child.ParentsAndChildren;
using WpfDormitories.DataBase.Entity.Contract;
using WpfDormitories.DataBase.Entity.District;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.Eviction;
using WpfDormitories.DataBase.Entity.Inventory;
using WpfDormitories.DataBase.Entity.Inventory.InventoryDirectory;
using WpfDormitories.DataBase.Entity.MenuElement;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase.Entity.User;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.DataBase.Repositories;
using WpfDormitories.Model.Users;


namespace WpfDormitories.DataBase
{
    public class DataManager
    {
        public IUserData CurrentUser { get; set; }
        public IRepository<IUserData> UsersRepository { get; set; }
        public IRepository<IChildData> ChildrenRepository { get; set; }
        public IRepository<IContractData> ContractsRepository { get; set; }
        public IRepository<IDistrictData> DistrictsRepository { get; set; }
        public IRepository<IDormData> DormsRepository { get; set; }
        public IRepository<IEvictionData> EvictionsRepository { get; set; }
        public IRepository<IInventoryDirectoryData> InventoryDirectoryRepository { get; set; }
        public IRepository<IInventoryData> InventoryRepository { get; set; }
        public IRepository<IParentsAndChildrenData> ParentsAndChildrenRepository { get; set; }
        public IRepository<IResidentData> ResidentsRepository { get; set; }
        public IRepository<IRoomData> RoomsRepository { get; set; }
        public IRepository<IStreetData> StreetsRepository { get; set; }
        public IRepository<IUserAbilitiesData> UsersAbilitiesRepository { get; set; }
        public IRepository<IMenuElementData> MenuElementsRepository { get; set; }


        public void Inject
            (IRepository<IUserData> usersRepository, IRepository<IChildData> childrenRepository, IRepository<IContractData> contractsRepository,
            IRepository<IDistrictData> districtsRepository, IRepository<IDormData> dormsRepository, IRepository<IEvictionData> evictionsRepository,
            IRepository<IInventoryDirectoryData> inventoryDirectoryRepository, IRepository<IInventoryData> inventoryRepository, 
            IRepository<IParentsAndChildrenData> parentsAndChildrenRepository, IRepository<IResidentData> residentsRepository,
            IRepository<IRoomData> roomsRepository, IRepository<IStreetData> streetsRepository, IRepository<IUserAbilitiesData> usersAbilitiesRepository,
            IRepository<IMenuElementData> menuElementsRepository) 
        {  
            UsersRepository = usersRepository;
            ChildrenRepository = childrenRepository;
            ContractsRepository = contractsRepository;
            DistrictsRepository = districtsRepository;
            DormsRepository = dormsRepository;
            EvictionsRepository = evictionsRepository;
            InventoryDirectoryRepository = inventoryDirectoryRepository;
            InventoryRepository = inventoryRepository;
            ParentsAndChildrenRepository = parentsAndChildrenRepository;
            RoomsRepository = roomsRepository;
            StreetsRepository = streetsRepository;
            UsersAbilitiesRepository = usersAbilitiesRepository;
            MenuElementsRepository = menuElementsRepository;
        }

        private static DataManager _instance;
        public static DataManager GetInstance()
        {
            if (_instance == null)
                _instance = new DataManager();
            return _instance;
        }
    }
}
