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
using WpfDormitories.ViewModel;
using WpfDormitories.ViewModel.ChildrenVM;
using WpfDormitories.ViewModel.DormsVM;

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для ChildrenWindow.xaml
    /// </summary>
    public partial class ChildrenWindow : Window
    {
        public ChildrenWindow()
        {
            InitializeComponent();
            DataContextChanged += DormsWindow_DataContextChanged;
        }

        private void DormsWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is ChildrenViewModel childrenVM)
            {
                childrenVM.OnAdd += () =>
                {
                    AddOrEditChildWindow window = new();
                    window.DataContext = new AddChildViewModel();
                    window.ShowDialog();
                    childrenVM.UpdateTable();
                };
                childrenVM.OnEdit += (dataRow) =>
                {
                    AddOrEditChildWindow window = new();
                    window.DataContext = new EditChildViewModel((uint)dataRow[0], (string)dataRow[1], (DateTime)dataRow[2], (IFullName)dataRow[3]);
                    window.ShowDialog();
                    childrenVM.UpdateTable();
                };
                childrenVM.OnDelete += () =>
                {
                    ConfirmationWindow window = new ConfirmationWindow();
                    window.ShowDialog();
                    if (window.DataContext is ConfirmationViewModel confirmation)
                    {
                        childrenVM.DeleteConfirmStatus = confirmation.Result;
                    }
                };
                childrenVM.OnParents += (userAbility, dataRow) =>
                {
                    //ResidentsWindow window = new();
                    //window.DataContext = new ResidentsViewModel(userAbility, (uint)dataRow[0]);
                    //window.ShowDialog();
                    //contractVM.UpdateTable();
                };
            }
        }
    }
}
