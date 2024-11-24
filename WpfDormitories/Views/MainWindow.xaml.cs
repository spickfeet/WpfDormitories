using System.Windows;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.ViewModel;
using WpfDormitories.ViewModel.DirectoriesVM.DistrictsVM;
using WpfDormitories.ViewModel.DirectoriesVM.InventoriesVM;
using WpfDormitories.ViewModel.DirectoriesVM.StreetVM;
using WpfDormitories.ViewModel.DormsVM;
using WpfDormitories.ViewModel.UserAbilitiesVM;

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            MenuBuilder menuBuilder = new(this);
            Menu.Children.Add(menuBuilder.BuildMenu());
            if(DataContext is MainWindowViewModel mainWindowViewModel)
            {
                mainWindowViewModel.OnDelete += () =>
                {
                    ConfirmationWindow window = new ConfirmationWindow();
                    window.ShowDialog();
                    if (window.DataContext is ConfirmationViewModel confirmation)
                    {
                        mainWindowViewModel.DeleteConfirmStatus = confirmation.Result;
                    }
                };
                mainWindowViewModel.OnAboutProgram += () =>
                {
                    AboutProgramWindow window = new();
                    window.ShowDialog();
                };

                mainWindowViewModel.OnMenuContents += () =>
                {
                    MenuContentsWindow window = new();
                    window.ShowDialog();
                };

                mainWindowViewModel.OnChangePassword += () =>
                {
                    ChangePasswordWindow window = new();
                    window.ShowDialog();
                };

                mainWindowViewModel.OnCustomQuery += () =>
                {
                    QueryWindow window = new();
                    window.ShowDialog();
                };

                mainWindowViewModel.OnAddStreet += () => 
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.DataContext = new AddStreetViewModel();
                    window.ShowDialog();
                    UpdateTable();
                };
                mainWindowViewModel.OnEditStreet += (dataRow) => 
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.DataContext = new EditStreetViewModel(uint.Parse(dataRow[0].ToString()), dataRow[1].ToString());
                    window.ShowDialog();
                    UpdateTable();
                };
                mainWindowViewModel.OnAddDistrict += () => 
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.DataContext = new AddDistrictViewModel();
                    window.ShowDialog();
                    UpdateTable();
                };
                mainWindowViewModel.OnEditDistrict += (dataRow) => 
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.DataContext = new EditDistrictViewModel(uint.Parse(dataRow[0].ToString()), dataRow[1].ToString());
                    window.ShowDialog();
                    UpdateTable();
                };
                mainWindowViewModel.OnAddInventoryDirectory += () =>
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.DataContext = new AddInventoryViewModel();
                    window.ShowDialog();
                    UpdateTable();
                };
                mainWindowViewModel.OnEditInventoryDirectory += (dataRow) =>
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.DataContext = new EditInventoryViewModel(uint.Parse(dataRow[0].ToString()), dataRow[1].ToString());
                    window.ShowDialog();
                    UpdateTable();
                };

                mainWindowViewModel.OnAddUserAbilities += () =>
                {
                    AddOrEditUserAbilitiesWindow window = new();
                    window.DataContext = new AddUserAbilitiesViewModel();
                    window.ShowDialog();
                    UpdateTable();
                };
                mainWindowViewModel.OnEditUserAbilities += (dataRow) =>
                {
                    AddOrEditUserAbilitiesWindow window = new();
                    window.DataContext = new EditUserAbilitiesViewModel(uint.Parse(dataRow[0].ToString()), uint.Parse(dataRow[1].ToString()), 
                        uint.Parse(dataRow[2].ToString()), (bool)dataRow[3], (bool)dataRow[4], (bool)dataRow[5], (bool)dataRow[6]);
                    window.ShowDialog();
                    UpdateTable();
                };
                mainWindowViewModel.OnDorms += (userAbilitiesData) =>
                {
                    DormsWindow window = new();
                    window.DataContext = new DormsViewModel(userAbilitiesData);
                    window.ShowDialog();
                };
            }
            
        }
        private void UpdateTable(/*object? sender, EventArgs e*/)
        {
            if (DataContext is MainWindowViewModel mainWindowViewModel) 
            {
                mainWindowViewModel.UpdateTable();
            }
        }
    }
}
