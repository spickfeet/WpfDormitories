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

namespace WpfDormitories.Views
{
    /// <summary>
    /// Логика взаимодействия для MenuContentsWindow.xaml
    /// </summary>
    public partial class MenuContentsWindow : Window
    {
        public MenuContentsWindow()
        {
            InitializeComponent();
            Text.Text = 
                " * Разное\n" +
                "      - Настройки\n" +
                "            - Права пользователей\n" +
                "            - Зарегистрировать нового пользователя\n" +
                "      - Сменить пароль\n" +
                "      - Запрос к БД\n" +
                " * Документы\n" +
                "      - Экспорт документов заселения\n" +
                " * Справочники\n" +
                "      - Улицы\n" +
                "      - Районы\n" +
                "      - Перечень инвентаря\n" +
                " * Общежития\n" +
                " * Заселения\n" +
                " * Дети\n" +
                " * Выселения\n" +
                " * Справка\n" +
                "      - О программе\n" +
                "      - Содержание";
        }
    }
}
