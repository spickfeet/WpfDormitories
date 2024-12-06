using WpfDormitories.Model.Users;

namespace WpfDormitories.DataBase.Entity.User
{
    public interface IUserData
    {
        uint Id { get; set; }
        IUser User { get; set; }
    }
}
