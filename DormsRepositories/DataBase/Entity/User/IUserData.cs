using WpfDormitories.Model.Users;

namespace WpfDormitories.DataBase.Entity.User
{
    public interface IUserData
    {
        string Surname { get; set; }
        string Name { get; set; }
        string Patronymic { get; set; }
        uint Id { get; set; }
        IUser User { get; set; }
    }
}
