﻿using System;
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

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditContractWindow.xaml
    /// </summary>
    public partial class AddOrEditContractWindow : Window
    {
        public AddOrEditContractWindow()
        {
            InitializeComponent();
            DataContextChanged += AddOrEditDirectoriesWindow_DataContextChanged;
        }

        private void AddOrEditDirectoriesWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is IApplicableVM applicable)
            {
                applicable.OnApply += () =>
                {
                    ConfirmationWindow window = new ConfirmationWindow();
                    window.ShowDialog();
                    if (window.DataContext is ConfirmationViewModel confirmation)
                    {
                        applicable.ConfirmApplyStatus = confirmation.Result;
                        if (applicable.ConfirmApplyStatus == true)
                        {
                            Close();
                        }
                    }
                };
            }
        }
    }
}