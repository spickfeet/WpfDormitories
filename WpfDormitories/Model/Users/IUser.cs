using System.ComponentModel;

namespace WpfDormitories.Model.Users
{
    public interface IUser
    {
        [DisplayName("Логин")]
        string Login { get; set; }
        [DisplayName("Пароль")]
        string Password { get; set; }
    }
}
