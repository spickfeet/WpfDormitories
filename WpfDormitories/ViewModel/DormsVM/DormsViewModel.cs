using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.Enums;
using WpfDormitories.Model.Services.Tables;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.DormsVM
{
    public class DormsViewModel : BasicVM
    {
        private ITableService _tableService;

        private int _selectedIndex;
        private DataTable _table;
        private string _findText;
        private Visibility _w;
        private Visibility _e;
        private Visibility _d;

        public bool DeleteConfirmStatus { get; set; }

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
            }
        }

        public Visibility W
        {
            get { return _w; }
            set
            {
                Set(ref _w, value);
            }
        }

        public Visibility E
        {
            get { return _e; }
            set
            {
                Set(ref _e, value);
            }
        }

        public Visibility D
        {
            get { return _d; }
            set
            {
                Set(ref _d, value);
            }
        }

        public DormsViewModel(IUserAbilitiesData userAbilities)
        {
            DeleteConfirmStatus = false;
            _tableService = new DormsTableService();
            _table = _tableService.Read();
            _w = userAbilities.W ? Visibility.Visible : Visibility.Collapsed;
            _e = userAbilities.E ? Visibility.Visible : Visibility.Collapsed;
            _d = userAbilities.D ? Visibility.Visible : Visibility.Collapsed;

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
