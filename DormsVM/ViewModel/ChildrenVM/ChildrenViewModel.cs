﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.DataBase;
using WpfDormitories.Model.Services.Tables;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.ChildrenVM
{
    public class ChildrenViewModel : BasicVM
    {
        private ITableService _tableService;

        private int _selectedIndex;
        private DataTable _table;
        private string _findText;

        private IUserAbilitiesData _userAbilitiesData;


        public bool DeleteConfirmStatus { get; set; }

        public Action<IUserAbilitiesData, DataRow> OnParents;
        public Action<DataRow> OnEdit;
        public Action OnAdd;
        public Action OnDelete;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                Set(ref _selectedIndex, value);
            }
        }

        public DataTable Table
        {
            get { return _table; }
            set
            {
                Set(ref _table, value);
            }
        }

        public string FindText
        {
            get { return _findText; }
            set
            {
                Set(ref _findText, value);
                if (_tableService != null)
                    Table = _tableService.FindAll(FindText);
            }
        }

        public Visibility W
        {
            get { return _userAbilitiesData.W ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility E
        {
            get { return _userAbilitiesData.E ? Visibility.Visible : Visibility.Collapsed; ; }
        }

        public Visibility D
        {
            get { return _userAbilitiesData.D ? Visibility.Visible : Visibility.Collapsed; ; }
        }

        public ChildrenViewModel(IUserAbilitiesData userAbilities)
        {
            _selectedIndex = -1;
            _userAbilitiesData = userAbilities;
            
            DeleteConfirmStatus = false;
            _tableService = new ChildrenTableService();

            _table = _tableService.Read();

            _tableService.OnEdit += (dataRow) => { OnEdit?.Invoke(dataRow); };
            _tableService.OnAdd += () => { OnAdd?.Invoke(); };
        }

        /// <summary>
        /// Обновить данные в таблице.
        /// </summary>
        public void UpdateTable()
        {
            Table = _tableService.Read();
        }

        /// <summary>
        /// Удалить ребенка.
        /// </summary>
        public ICommand Delete
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < _table.Rows.Count)
                    {
                        OnDelete?.Invoke();
                        if (DeleteConfirmStatus)
                        {
                            _tableService.Delete(SelectedIndex);
                            Table = _tableService.Read();
                            DeleteConfirmStatus = false;
                        }
                    }

                });

            }
        }

        /// <summary>
        /// Вызвать событие открытия окна изменения ребенка.
        /// </summary>
        public ICommand Edit
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < _table.Rows.Count)
                    {
                        _tableService.Edit(SelectedIndex);
                    }
                });

            }
        }

        /// <summary>
        /// Вызвать событие открытия окна добавления ребенка.
        /// </summary>
        public ICommand Add
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _tableService.Add();
                });

            }
        }

        /// <summary>
        /// Вызвать событие для открытия окна с родителями.
        /// </summary>
        public ICommand Parents
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < _table.Rows.Count)
                    {
                        OnParents?.Invoke(_userAbilitiesData, _tableService.GetByIndex(SelectedIndex));
                    }
                });

            }
        }
    }
}
