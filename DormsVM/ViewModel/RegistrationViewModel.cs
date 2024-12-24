using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.MenuElement;
using WpfDormitories.DataBase.Entity.User;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.Model.Convertors;
using WpfTest.Model.Services.Registration;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel
{
    public class RegistrationViewModel : BasicVM
    {
        private IRegistrationService _registrationService = new RegistrationService(new HashCodeConvertor());

        private string _surname;
        private string _name;
        private string _patronymic;

        private string _login;
        private string _password;
        private string _repeatPassword;

        public string Surname
        {
            get { return _surname; }
            set
            {
                Set(ref _surname, value);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
            }
        }

        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                Set(ref _patronymic, value);
            }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                Set(ref _login, value);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                Set(ref _password, value);
            }
        }

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set
            {
                _repeatPassword = value;
                Set(ref _repeatPassword, value);
            }
        }


        private void Register()
        {
            if (Password != RepeatPassword) return;
            if(_registrationService.TryRegistration(Surname, Name, Patronymic, Login, Password))
            {
                IUserData user = DataManager.GetInstance().UsersRepository.Read()[^1];
                IList<IMenuElementData> menuElements = DataManager.GetInstance().MenuElementsRepository.Read()
                    .ToList().FindAll(item => !string.IsNullOrEmpty(item.FuncName));
                foreach (IMenuElementData menuElement in menuElements)
                {
                    DataManager.GetInstance().UsersAbilitiesRepository.Create(new UserAbilitiesData(user.Id, menuElement.Id, false, false, false, false));
                }
                MessageBox.Show("Пользователь успешно зарегистрирован");
            }

        }

        public ICommand RegisterCommand
        {
            get
            {
                return new DelegateCommand(() => Register());
            }
        }
    }
}
