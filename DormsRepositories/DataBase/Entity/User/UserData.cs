using WpfDormitories.Model.Users;

namespace WpfDormitories.DataBase.Entity.User
{
    public class UserData : IUserData
    {
        public string Surname {  get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public uint Id { get; set; }

        public IUser User { get; set; }

        public UserData(uint id, string surname, string name, string patronymic, string login, string password) 
        {
            Id = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            User = new Model.Users.User(login, password);
        }

        public UserData(string surname, string name, string patronymic, string login, string password)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            User = new Model.Users.User(login, password);
        }
    }
}
