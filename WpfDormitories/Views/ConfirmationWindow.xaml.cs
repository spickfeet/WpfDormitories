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
    /// Логика взаимодействия для ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        public ConfirmationWindow()
        {
            InitializeComponent();
            DataContext = new ConfirmationViewModel();
            if (DataContext is ConfirmationViewModel confirmationVM) 
            {
                confirmationVM.OnConfirmed += () => { Close(); };
            }
        }
    }
}
