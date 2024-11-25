using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для RoomsWindow.xaml
    /// </summary>
    public partial class RoomsWindow : Window
    {
        public RoomsWindow()
        {
            InitializeComponent();
            DataContextChanged += DormsWindow_DataContextChanged;
        }

        private void DormsWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is RoomsViewModel dormsVM)
            {
                dormsVM.OnAdd += (dormId) =>
                {
                    AddOrEditRoomWindow window = new();
                    window.DataContext = new AddRoomViewModel(dormId);
                    window.ShowDialog();
                    dormsVM.UpdateTable();
                };
                dormsVM.OnEdit += (dataRow) =>
                {
                    AddOrEditRoomWindow window = new();
                    window.DataContext = new EditRoomViewModel((uint)dataRow[0], (uint)dataRow[1], (string)dataRow[2],
                        (uint)dataRow[3], (uint)dataRow[4], (uint)dataRow[5], (uint)dataRow[6]);
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
                dormsVM.OnInventory += (userAbility, dataRow) =>
                {
                    //RoomsWindow window = new();
                    //window.DataContext = new RoomsViewModel(userAbility, (uint)dataRow[0]);
                    //window.ShowDialog();
                    //dormsVM.UpdateTable();
                };
            }
        }
    }
}
