using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.User;
using WpfDormitories.DataBase.Repositories;
using WpfDormitories.Model.Convertors;

namespace WpfTest.Model.Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private IConvertor<string, string> _convertor;
        public RegistrationService(IConvertor<string, string> convertor) 
        {
            _convertor = convertor;
        }
        public bool TryRegistration(string login, string password)
        {
            foreach(IUserData userData in DataManager.GetInstance().UsersRepository.Read())
            {
                if(userData.Login == login)
                {
                    return false;
                }
            }
            DataManager.GetInstance().UsersRepository.Create(new UserData(0, login, _convertor.Convert(password)));
            return true;
        }
    }
}
