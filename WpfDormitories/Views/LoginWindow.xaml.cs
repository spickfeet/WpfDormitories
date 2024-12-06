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
            Enter.Click += Enter_Click;
            Cancel.Click += Cancel_Click;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel loginViewModel)
            {
                loginViewModel.Password = Password.Password;
            }
        }

        public void LogInSuccess()
        {
            MainWindow window = new();
            window.Show();
            Close();
        }
    }
}