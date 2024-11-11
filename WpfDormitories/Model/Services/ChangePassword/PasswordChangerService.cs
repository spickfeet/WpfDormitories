using WpfDormitories.DataBase;
using WpfDormitories.Model.Convertors;

namespace WpfDormitories.Model.Services.ChangePassword
{
    public class PasswordChangerService : IPasswordChangerService
    {
        private IConvertor<string, string> _convertor;
        public PasswordChangerService(IConvertor<string,string> convertor)
        {
            _convertor = convertor;
        }
        public void Change(string oldPassword, string newPassword)
        {
            if (_convertor.Convert(oldPassword) == DataManager.GetInstance().CurrentUser.User.Password)
            {
                DataManager.GetInstance().CurrentUser.User.Password = _convertor.Convert(newPassword);
                DataManager.GetInstance().UsersRepository.Update(DataManager.GetInstance().CurrentUser);
            }
            else 
            {
                throw new ArgumentException("Неверный текущий пароль");
            }
        }
    }
}
