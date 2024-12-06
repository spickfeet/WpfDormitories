using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfDormitories.ViewModel;

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
            Enter.Click += Enter_Click;
            Cancel.Click += Cancel_Click;
            if (DataContext is IApplicableVM applicable)
            {
                applicable.OnApply += () =>
                {
                    ConfirmationWindow window = new ConfirmationWindow();
                    window.ShowDialog();
                    if (window.DataContext is ConfirmationViewModel confirmation)
                    {
                        applicable.ConfirmApplyStatus = confirmation.Result;
                    }
                };
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChangePasswordViewModel changePasswordVM)
            {
                changePasswordVM.NewPassword = NewPassword.Password;
                changePasswordVM.OldPassword = OldPassword.Password;
                changePasswordVM.RepeatNewPassword = RepeatNewPassword.Password;
            }
        }
    }
}
