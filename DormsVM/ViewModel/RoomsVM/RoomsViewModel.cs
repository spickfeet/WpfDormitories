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
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Dorm;

namespace WpfDormitories.ViewModel.RoomsVM
{
    public class RoomsViewModel : BasicVM
    {
        private ITableService _tableService;
        private string _dormInfo;

        private int _selectedIndex;
        private DataTable _table;
        private string _findText;

        private IUserAbilitiesData _userAbilitiesData;


        public bool DeleteConfirmStatus { get; set; }

        public Action<IUserAbilitiesData,DataRow> OnInventory;
        public Action<DataRow> OnEdit;
        public Action<uint> OnAdd;
        public Action OnDelete;

        public string DormInfo
        {
            get { return _dormInfo; }
            set
            {
                Set(ref _dormInfo, value);
            }
        }

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

        public RoomsViewModel(IUserAbilitiesData userAbilities, uint dormId)
        {
            _selectedIndex = -1;
            _userAbilitiesData = userAbilities;
            List<IDormData> dorms = DataManager.GetInstance().DormsRepository.Read().ToList();
            IDormData dorm = dorms.Find(item => item.Id == dormId);
            _dormInfo = $"Общежитие №{dorm.DormNumber} {DataManager.GetInstance().StreetsRepository.Read().ToList().Find(item => item.Id == dorm.StreetId).Name}";
            DeleteConfirmStatus = false;
            _tableService = new RoomsTableService(dormId);

            _table = _tableService.Read();

            _tableService.OnEdit += (dataRow) => { OnEdit?.Invoke(dataRow); };
            _tableService.OnAdd += () => { OnAdd?.Invoke(dormId); };
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

        public ICommand Inventory
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedIndex >= 0 && SelectedIndex < _table.Rows.Count)
                    {
                        OnInventory?.Invoke(_userAbilitiesData, _tableService.GetByIndex(SelectedIndex));
                    }
                });

            }
        }
    }
}
