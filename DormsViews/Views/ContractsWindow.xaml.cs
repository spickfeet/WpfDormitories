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
using WpfDormitories.ViewModel.ResidentsVM;
using WpfDormitories.ViewModel.RoomsVM;

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для ContractsWindow.xaml
    /// </summary>
    public partial class ContractsWindow : Window
    {
        public ContractsWindow()
        {
            InitializeComponent();
            DataContextChanged += DormsWindow_DataContextChanged;
        }

        private void DormsWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is ContractsViewModel contractVM)
            {
                contractVM.OnAdd += () =>
                {
                    AddOrEditContractWindow window = new();
                    window.DataContext = new AddContractViewModel();
                    window.ShowDialog();
                    contractVM.UpdateTable();
                };
                contractVM.OnEdit += (dataRow) =>
                {
                    AddOrEditContractWindow window = new();
                    window.DataContext = new EditContractViewModel((uint)dataRow[0], (string)dataRow[1], (string)dataRow[2], (string)dataRow[3], (DateTime)dataRow[4], (string)dataRow[5]);
                    window.ShowDialog();
                    contractVM.UpdateTable();
                };
                contractVM.OnDelete += () =>
                {
                    ConfirmationWindow window = new ConfirmationWindow();
                    window.ShowDialog();
                    if (window.DataContext is ConfirmationViewModel confirmation)
                    {
                        contractVM.DeleteConfirmStatus = confirmation.Result;
                    }
                };
                contractVM.OnResidents += (userAbility, dataRow) =>
                {
                    ResidentsWindow window = new();
                    window.DataContext = new ResidentsViewModel(userAbility, (uint)dataRow[0]);
                    window.ShowDialog();
                    contractVM.UpdateTable();
                };
            }
        }
    }
}
