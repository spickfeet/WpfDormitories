using System.Data;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.Model.Services.Tables;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.DormsVM
{
    public class ContractsViewModel : BasicVM
    {
        private ITableService _tableService;

        private int _selectedIndex;
        private DataTable _table;
        private string _findText;

        private IUserAbilitiesData _userAbilitiesData;

        private Visibility _haveComment;
        private string _comment;

        public bool DeleteConfirmStatus { get; set; }

        public Action<IUserAbilitiesData, DataRow> OnResidents;
        public Action<DataRow> OnEdit;
        public Action OnAdd;
        public Action OnDelete;

        public Visibility HaveComment
        {
            get { return _haveComment; }
            set
            {
                Set(ref _haveComment, value);
            }
        }
        public string Comment
        {
            get { return _comment; }
            set
            {
                Set(ref _comment, value);
                if (!string.IsNullOrEmpty(value))
                {
                    HaveComment = Visibility.Visible;
                }
                else
                {
                    HaveComment = Visibility.Collapsed;
                }
            }
        }


        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                Set(ref _selectedIndex, value);
                if (value >= 0)
                    Comment = (string)_tableService.GetByIndex(_selectedIndex)[5];
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

        public ContractsViewModel(IUserAbilitiesData userAbilities)
        {
            _haveComment = Visibility.Collapsed;
            _selectedIndex = -1;
            _userAbilitiesData = userAbilities;
            DeleteConfirmStatus = false;
            _tableService = new ContractsTableService();

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
        /// Удалить данные о договоре.
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
        /// Вызвать событие для открытия окна изменений данных о выбранном договоре.
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
        /// Вызвать событие открытия окна для добавления нового нового договора.
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
        /// Вызвать событие для открытия окна с жильцами по этому договору.
        /// </summary>
        public ICommand Residents
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < _table.Rows.Count)
                    {
                        OnResidents?.Invoke(_userAbilitiesData, _tableService.GetByIndex(SelectedIndex));
                    }
                });

            }
        }
    }
}
