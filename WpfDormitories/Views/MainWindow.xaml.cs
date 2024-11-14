using System.Windows;
using WpfDormitories.ViewModel;
using WpfDormitories.ViewModel.DirectoriesVM.DistrictsVM;
using WpfDormitories.ViewModel.DirectoriesVM.InventoriesVM;
using WpfDormitories.ViewModel.DirectoriesVM.StreetVM;

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

                mainWindowViewModel.OnSettings += () =>
                {
                    MessageBox.Show("В процессе");
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
