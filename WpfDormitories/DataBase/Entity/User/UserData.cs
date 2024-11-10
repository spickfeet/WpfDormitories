using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.Model.Users;

namespace WpfDormitories.DataBase.Entity.User
{
    class UserData : IUserData
    {
        public uint Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public UserData(uint id, string login, string password) 
        {
            Id = id;
            Login = login;
            Password = password;
        }
        public UserData(uint id, IUser user)
        {
            Id = id;
            Login = user.Login;
            Password = user.Password;
        }
    }
}
