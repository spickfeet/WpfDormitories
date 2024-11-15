using System.Windows;
using WpfDormitories.ViewModel;
using WpfDormitories.ViewModel.DirectoriesVM.DistrictsVM;
using WpfDormitories.ViewModel.DirectoriesVM.InventoriesVM;
using WpfDormitories.ViewModel.DirectoriesVM.StreetVM;
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

                mainWindowViewModel.OnAddStreet += () => 
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.UpdateDataContext(new AddStreetViewModel());
                    window.Show();
                    window.Closing += UpdateTable;
                };
                mainWindowViewModel.OnEditStreet += (dataRow) => 
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.UpdateDataContext(new EditStreetViewModel(uint.Parse(dataRow[0].ToString()), dataRow[1].ToString()));
                    window.Show();
                    window.Closing += UpdateTable;
                };
                mainWindowViewModel.OnAddDistrict += () => 
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.UpdateDataContext(new AddDistrictViewModel());
                    window.Show();
                    window.Closing += UpdateTable;
                };
                mainWindowViewModel.OnEditDistrict += (dataRow) => 
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.UpdateDataContext(new EditDistrictViewModel(uint.Parse(dataRow[0].ToString()), dataRow[1].ToString()));
                    window.Show();
                    window.Closing += UpdateTable;
                };
                mainWindowViewModel.OnAddInventoryDirectory += () =>
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.UpdateDataContext(new AddInventoryViewModel());
                    window.Show();
                    window.Closing += UpdateTable;
                };
                mainWindowViewModel.OnEditInventoryDirectory += (dataRow) =>
                {
                    AddOrEditDirectoriesWindow window = new();
                    window.UpdateDataContext(new EditInventoryViewModel(uint.Parse(dataRow[0].ToString()), dataRow[1].ToString()));
                    window.Show();
                    window.Closing += UpdateTable;
                };

                mainWindowViewModel.OnAddUserAbilities += () =>
                {
                    AddOrEditUserAbilitiesWindow window = new();
                    window.UpdateDataContext(new AddUserAbilitiesViewModel());
                    window.Show();
                    window.Closing += UpdateTable;
                };
                mainWindowViewModel.OnEditUserAbilities += (dataRow) =>
                {
                    AddOrEditUserAbilitiesWindow window = new();
                    window.UpdateDataContext(new EditUserAbilitiesViewModel(uint.Parse(dataRow[0].ToString()), 
                        uint.Parse(dataRow[1].ToString()), uint.Parse(dataRow[2].ToString()),
                        (bool)dataRow[3], (bool)dataRow[4], (bool)dataRow[5], (bool)dataRow[6]));
                    window.Show();
                    window.Closing += UpdateTable;
                };
            }
            
        }
        private void UpdateTable(object? sender, EventArgs e)
        {
            if (DataContext is MainWindowViewModel mainWindowViewModel) 
            {
                mainWindowViewModel.UpdateTable();
            }
        }
    }
}
