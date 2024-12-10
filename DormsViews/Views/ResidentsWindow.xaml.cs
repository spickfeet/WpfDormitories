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
using WpfDormitories.Model.FullName;
using WpfDormitories.Model.PersonDocument.Passport;
using WpfDormitories.Model.PersonDocument;
using WpfDormitories.ViewModel;
using WpfDormitories.ViewModel.ResidentsVM;
using WpfDormitories.ViewModel.RoomsVM;
using WpfDormitories.ViewModel.EvictionsVM;

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для ResidentsWindow.xaml
    /// </summary>
    public partial class ResidentsWindow : Window
    {
        public ResidentsWindow()
        {
            InitializeComponent();
            DataContextChanged += DormsWindow_DataContextChanged;
        }

        private void DormsWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is ResidentsViewModel residentsVM)
            {
                residentsVM.OnAdd += (contractId) =>
                {
                    AddOrEditResidentWindow window = new();
                    window.DataContext = new AddResidentViewModel(contractId);
                    window.ShowDialog();
                    residentsVM.UpdateTable();
                };
                residentsVM.OnEdit += (dataRow) =>
                {
                    AddOrEditResidentWindow window = new();
                    window.DataContext = new EditResidentViewModel((uint)dataRow[0],
                        (uint)dataRow[1],
                        (uint)dataRow[2],
                        (PersonDocuments)dataRow[3],
                        (bool)dataRow[4],
                        (DateTime)dataRow[5],
                        (float)dataRow[6],
                        dataRow[7]?.ToString(),
                        dataRow[8]?.ToString());
                    window.ShowDialog();
                    residentsVM.UpdateTable();
                };
                residentsVM.OnDelete += () =>
                {
                    ConfirmationWindow window = new ConfirmationWindow();
                    window.ShowDialog();
                    if (window.DataContext is ConfirmationViewModel confirmation)
                    {
                        residentsVM.DeleteConfirmStatus = confirmation.Result;
                    }
                };
                residentsVM.OnEvict += (residentId) =>
                {
                    EditEvictionWindow window = new();
                    window.DataContext = new AddEvictionViewModel(residentId);
                    window.ShowDialog();
                    residentsVM.UpdateTable();
                };
            }
        }
    }
}
