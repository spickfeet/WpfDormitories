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
using WpfDormitories.Model.PersonDocument;
using WpfDormitories.ViewModel.EvictionsVM;

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для EvictionsWindow.xaml
    /// </summary>
    public partial class EvictionsWindow : Window
    {
        public EvictionsWindow()
        {
            InitializeComponent();
            DataContextChanged += AddOrEditDormWindow_DataContextChanged;
        }

        private void AddOrEditDormWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is EvictionsViewModel evictionsVM)
            {
                evictionsVM.OnAdd += () =>
                {
                    AddEvictionWindow window = new();
                    window.DataContext = new AddEvictionViewModel();
                    window.ShowDialog();
                    evictionsVM.UpdateTable();
                };

                evictionsVM.OnEdit += (dataRow) =>
                {
                    EditEvictionWindow window = new();
                    window.DataContext = new EditEvictionViewModel((uint)dataRow[0], (IPersonDocuments)dataRow[1], 
                        (string)dataRow[2], (DateTime)dataRow[3]);
                    window.ShowDialog();
                    evictionsVM.UpdateTable();
                };
            }
        }
    }
}
