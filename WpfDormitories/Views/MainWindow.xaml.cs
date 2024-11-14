using System.Windows;
using WpfDormitories.ViewModel;
using WpfDormitories.ViewModel.DirectoriesVM.DistrictsVM;
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
                mainWindowViewModel.OnAddStreet += () => 
                {
                    AddOrEditDirectoriesWindow window = new AddOrEditDirectoriesWindow();
                    window.UpdateDataContext(new AddStreetViewModel());
                    window.Show();
                    window.Closing += UpdateTable;
                };
                mainWindowViewModel.OnEditStreet += (dataRow) => 
                {
                    AddOrEditDirectoriesWindow window = new AddOrEditDirectoriesWindow();
                    window.UpdateDataContext(new EditStreetViewModel(uint.Parse(dataRow[0].ToString()), dataRow[1].ToString()));
                    window.Show();
                    window.Closing += UpdateTable;
                };
                mainWindowViewModel.OnAddDistrict += () => 
                {
                    AddOrEditDirectoriesWindow window = new AddOrEditDirectoriesWindow();
                    window.UpdateDataContext(new AddDistrictViewModel());
                    window.Show();
                    window.Closing += UpdateTable;
                };
                mainWindowViewModel.OnEditDistrict += (dataRow) => 
                {
                    AddOrEditDirectoriesWindow window = new AddOrEditDirectoriesWindow();
                    window.UpdateDataContext(new EditDistrictViewModel(uint.Parse(dataRow[0].ToString()), dataRow[1].ToString()));
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
