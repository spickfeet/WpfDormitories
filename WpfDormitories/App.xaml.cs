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
            DataManager.GetInstance().Inject(new UsersRepository(), new ChildrenRepository(), new ContractsRepository(),
                new DistrictsRepository(), new DormsRepository(), new EvictionsRepository(), new InventoryDirectoryRepository(),
                new InventoryRepository(), new ParentsAndChildrenRepository(), new ResidentsRepository(), new RoomsRepository(),
                new StreetsRepository(), new UserAbilitiesRepository());
            LoginWindow window = new LoginWindow();
            app.Run(window);
        }
    }

}
