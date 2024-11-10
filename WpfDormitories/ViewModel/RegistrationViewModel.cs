using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfDormitories.Model.Convertors;
using WpfDormitories.ViewModel;
using WpfTest.Model.Services.Registration;

namespace WpfTest.ViewModel
{
    public class RegistrationViewModel : BasicVM
    {
        private IRegistrationService _registrationService = new RegistrationService(new HashCodeConvertor());

        private string _login;
        private string _password;
        private string _repeatPassword;

        public string Login
        {
            get { return _login; }
            set 
            {
                _login = value;
                Set<string>(ref _login, value);
            }
        }

        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                Set<string>(ref _password, value);
            }
        }

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set 
            { 
                _repeatPassword = value;
                Set<string>(ref _repeatPassword, value);
            }
        }


        private void Register()
        {
            if (Password != RepeatPassword) return;
            _registrationService.TryRegistration(Login, Password);
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
