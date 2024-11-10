using System.Windows;
using WpfTest.ViewModel;

namespace WpfDormitories.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
            if (DataContext is LoginViewModel loginViewModel) 
            {
                loginViewModel.OnLogInSuccess += LogInSuccess;
            }
        }

        public void LogInSuccess()
        {
            ChangePasswordWindow window = new();
            window.Show();
            Close();
        }
    }
}