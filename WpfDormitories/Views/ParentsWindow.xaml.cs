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
using WpfDormitories.Model.Services.Tables;
using WpfDormitories.ViewModel.EvictionsVM;
using WpfDormitories.ViewModel.ParentsVM;

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для ParentsWindow.xaml
    /// </summary>
    public partial class ParentsWindow : Window
    {
        public ParentsWindow()
        {
            InitializeComponent();
            DataContextChanged += AddOrEditDormWindow_DataContextChanged;
        }

        private void AddOrEditDormWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is ParentsViewModel parentsVM)
            {
                parentsVM.OnAdd += (childId) =>
                {
                    AddOrEditParentsWindow window = new();
                    window.DataContext = new AddParentViewModel(childId);
                    window.ShowDialog();
                    parentsVM.UpdateTable();
                };

                parentsVM.OnEdit += (dataRow) =>
                {
                    AddOrEditParentsWindow window = new();
                    window.DataContext = new EditParentViewModel((uint)dataRow[0], (uint)dataRow[1], (uint)dataRow[2]);
                    window.ShowDialog();
                    parentsVM.UpdateTable();
                };
            }
        }
    }
}
