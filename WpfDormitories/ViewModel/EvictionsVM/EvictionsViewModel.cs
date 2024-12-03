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
using System.Xml.Linq;

namespace WpfDormitories.ViewModel.EvictionsVM
{
    public class EvictionsViewModel : BasicVM
    {
        private ITableService _tableService;

        private int _selectedIndex;
        private DataTable _table;
        private string _findText;

        private string _reason;
        private Visibility _haveReason;

        private IUserAbilitiesData _userAbilitiesData;

        public bool DeleteConfirmStatus { get; set; }

        public Action<DataRow> OnEdit;
        public Action OnAdd;
        public Action OnDelete;

        public Visibility HaveReason
        {
            get { return _haveReason; }
            set
            {
                Set(ref _haveReason, value);
            }
        }

        public string Reason
        {
            get { return _reason; }
            set
            {
                Set(ref _reason, value);

                if (!string.IsNullOrEmpty(value))
                {
                    HaveReason = Visibility.Visible;
                }
                else
                {
                    HaveReason = Visibility.Collapsed;
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
                    Reason = (string)_tableService.GetByIndex(_selectedIndex)[2];
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

        public EvictionsViewModel(IUserAbilitiesData userAbilities)
        {
            HaveReason = Visibility.Collapsed;
            _selectedIndex = -1;
            _userAbilitiesData = userAbilities;
            DeleteConfirmStatus = false;
            _tableService = new EvictionsTableService();

            _table = _tableService.Read();

            _tableService.OnEdit += (dataRow) => { OnEdit?.Invoke(dataRow); };
            _tableService.OnAdd += () => { OnAdd?.Invoke(); };
        }

        public void UpdateTable()
        {
            Table = _tableService.Read();
        }

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

        public ICommand FindAll
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Table = _tableService.FindAll(FindText);
                });

            }
        }

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
