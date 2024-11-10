using System.Windows;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Repositories;
using WpfDormitories.Views;

namespace WpfDormitories
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        static void Main()
        {
            App app = new App();
            DataManager.GetInstance().Inject(new UsersRepository());
            LoginWindow window = new LoginWindow();
            app.Run(window);
        }
    }

}
