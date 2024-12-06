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
using WpfDormitories.ViewModel.InventoryVM;
using WpfDormitories.ViewModel.RoomsVM;

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window
    {
        public InventoryWindow()
        {
            InitializeComponent();
            DataContextChanged += DormsWindow_DataContextChanged;
        }

        private void DormsWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is InventoryViewModel inventoryVM)
            {
                inventoryVM.OnAdd += (roomId) =>
                {
                    AddOrEditInventoryWindow window = new();
                    window.DataContext = new AddInventoryViewModel(roomId);
                    window.ShowDialog();
                    inventoryVM.UpdateTable();
                };
                inventoryVM.OnEdit += (dataRow) =>
                {
                    AddOrEditInventoryWindow window = new();
                    window.DataContext = new EditInventoryViewModel((uint)dataRow[0], (uint)dataRow[1], (uint)dataRow[2]);
                    window.ShowDialog();
                    inventoryVM.UpdateTable();
                };
                inventoryVM.OnDelete += () =>
                {
                    ConfirmationWindow window = new ConfirmationWindow();
                    window.ShowDialog();
                    if (window.DataContext is ConfirmationViewModel confirmation)
                    {
                        inventoryVM.DeleteConfirmStatus = confirmation.Result;
                    }
                };
            }
        }
    }
}
