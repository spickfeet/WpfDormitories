using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.User;
using WpfDormitories.DataBase.Repositories;
using WpfDormitories.Model.Users;


namespace WpfDormitories.DataBase
{
    public class DataManager
    {
        public IUserData CurrentUser { get; set; }
        public IRepository<IUserData> UsersRepository { get; set; }

        public void Inject(IRepository<IUserData> usersRepository) 
        {  
            UsersRepository = usersRepository;
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
