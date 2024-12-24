using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.Model.Services.Tables;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.InventoryVM
{
    public class InventoryViewModel : BasicVM
    {
        private uint _roomId;

        private ITableService _tableService;

        private int _selectedIndex;
        private DataTable _table;
        private string _findText;

        private IUserAbilitiesData _userAbilitiesData;

        public bool DeleteConfirmStatus { get; set; }

        public Action<DataRow> OnEdit;
        public Action<uint> OnAdd;
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

        public InventoryViewModel(IUserAbilitiesData userAbilities, uint roomId)
        {
            _selectedIndex = -1;
            _roomId = roomId;
            _userAbilitiesData = userAbilities;
            DeleteConfirmStatus = false;
            _tableService = new InventoryTableService(_roomId);

            _table = _tableService.Read();

            _tableService.OnEdit += (dataRow) => { OnEdit?.Invoke(dataRow); };
            _tableService.OnAdd += () => { OnAdd?.Invoke(_roomId); };
        }

        /// <summary>
        /// Обновить таблицу с данными.
        /// </summary>
        public void UpdateTable()
        {
            Table = _tableService.Read();
        }

        /// <summary>
        /// Удалить данные о инвентаре.
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
        /// Вызвать событие открытия окна для изменения данных о инвентаре.
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
        /// Вызвать событие открытия окна для добавления данных о инвентаре.
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
    }
}
