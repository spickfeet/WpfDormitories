using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.Model.Convertors;
using WpfDormitories.Model.Services.ChangePassword;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel
{
    public class ChangePasswordViewModel : BasicVM
    {
        private string _oldPassword;
        private string _newPassword;
        private string _repeatNewPassword;
        private IPasswordChangerService _passwordChanger = new PasswordChangerService(new HashCodeConvertor());
        public string OldPassword 
        {  
            get { return _oldPassword; } 
            set 
            { 
                _oldPassword = value;
                Set<string>(ref _oldPassword, value);
            } 
        }
        public string NewPassword
        {
            get { return _newPassword; }
            set 
            { 
                _newPassword = value;
                Set<string>(ref _newPassword, value);
            }
        }
        public string RepeatNewPassword
        {
            get { return _repeatNewPassword; }
            set 
            { 
                _repeatNewPassword = value;
                Set<string>(ref _repeatNewPassword, value);
            }
        }

        public void ChangePassword()
        {
            try
            {
                if (RepeatNewPassword == NewPassword)
                {
                    _passwordChanger.Change(OldPassword, NewPassword);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ICommand ChangeCommand
        {
            get
            {
                return new DelegateCommand(() => ChangePassword());
            }
        }
    }
}
