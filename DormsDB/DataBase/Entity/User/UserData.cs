using WpfDormitories.Model.Users;

namespace WpfDormitories.DataBase.Entity.User
{
    class UserData : IUserData
    {
        public uint Id { get; set; }

        public IUser User { get; set; }

        public UserData(uint id, string login, string password) 
        {
            Id = id;
            User = new Model.Users.User(login, password);
        }

        public UserData(string login, string password)
        {
            User = new Model.Users.User(login, password);
        }
    }
}
