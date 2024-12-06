using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using WpfDormitories.ViewModel.DormsVM;
using WpfDormitories.ViewModel.RoomsVM;

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для DormsWindow.xaml
    /// </summary>
    public partial class DormsWindow : Window
    {
        public DormsWindow()
        {
            InitializeComponent();
            DataContextChanged += DormsWindow_DataContextChanged;
        }

        private void DormsWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is DormsViewModel dormsVM)
            {
                dormsVM.OnAdd += () =>
                {
                    AddOrEditDormWindow window = new();
                    window.DataContext = new AddDormViewModel();
                    window.ShowDialog();
                    dormsVM.UpdateTable();
                };
                dormsVM.OnEdit += (dataRow) =>
                {
                    AddOrEditDormWindow window = new();
                    window.DataContext = new EditDormViewModel((uint)dataRow[0], (uint)dataRow[1], (uint)dataRow[2],
                        (string)dataRow[3], (string)dataRow[4], (uint)dataRow[5], (uint)dataRow[6]);
                    window.ShowDialog();
                    dormsVM.UpdateTable();
                };
                dormsVM.OnDelete += () =>
                {
                    ConfirmationWindow window = new ConfirmationWindow();
                    window.ShowDialog();
                    if (window.DataContext is ConfirmationViewModel confirmation)
                    {
                        dormsVM.DeleteConfirmStatus = confirmation.Result;
                    }
                };
                dormsVM.OnRooms += (userAbility, dataRow) =>
                {
                    RoomsWindow window = new();
                    window.DataContext = new RoomsViewModel(userAbility, (uint)dataRow[0]);
                    window.ShowDialog();
                    dormsVM.UpdateTable();
                };
            }
        }
    }
}
