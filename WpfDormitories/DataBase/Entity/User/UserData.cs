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
        public UserData(uint id, IUser user)
        {
            Id = id;
            User = new Model.Users.User(user.Login, user.Password);
        }
    }
}
