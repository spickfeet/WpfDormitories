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

        /// <summary>
        /// Попытаться зарегистрироваться.
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool TryRegistration(string surname, string name, string patronymic, string login, string password)
        {
            foreach(IUserData userData in DataManager.GetInstance().UsersRepository.Read())
            {
                if(userData.User.Login == login)
                {
                    return false;
                }
            }
            DataManager.GetInstance().UsersRepository.Create(new UserData(surname, name, patronymic, login, _convertor.Convert(password)));
            return true;
        }
    }
}
