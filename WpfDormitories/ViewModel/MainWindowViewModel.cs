using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.District;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.Enums;
using WpfDormitories.Model.Services;
using WpfDormitories.Model.Services.Tables;
using WpfDormitories.TemporarySolutions;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel
{
    public class MainWindowViewModel : BasicVM
    {
        private DataTable _table;
        private bool _w;
        private bool _e;
        private bool _d;
        private int _selectedIndex;
        private string _findName;

        private UserAbilitiesService _abilitiesService;

        public Action OnMenuContents;
        public Action OnAboutProgram;
        public Action OnChangePassword;

        private ITableService _currentTable;
        private IDictionary<Tables,ITableService> _tableServices;

        public Action<DataRow> OnEditStreet;
        public Action OnAddStreet;

        public Action<DataRow> OnEditDistrict;
        public Action OnAddDistrict;

        public Action<DataRow> OnEditInventoryDirectory;
        public Action OnAddInventoryDirectory;

        public Action<DataRow> OnEditUserAbilities;
        public Action OnAddUserAbilities;

        public DataTable Table
        {
            get { return _table; }
            set
            {
                Set<DataTable>(ref _table, value);
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                Set<int>(ref _selectedIndex, value);
            }
        }

        public string FindName
        {
            get { return _findName; }
            set
            {
                Set<string>(ref _findName, value);
            }
        }

        public bool W
        {
            get { return _w; }
            set
            {
                Set<bool>(ref _w, value);
            }
        }

        public bool E
        {
            get { return _e; }
            set
            {
                Set<bool>(ref _e, value);
            }
        }

        public bool D
        {
            get { return _d; }
            set
            {
                Set<bool>(ref _d, value);
            }
        }


        public MainWindowViewModel()
        {
            _abilitiesService = new();

            _tableServices = new Dictionary<Tables, ITableService>();

            _tableServices.Add(Tables.Street, new StreetsTableService());
            _tableServices.Add(Tables.Districts, new DistrictsTableService());
            _tableServices.Add(Tables.InventoryDirectories, new InventoryDirectoryTableService());
            _tableServices.Add(Tables.UsersAbilities, new UsersAbilitiesTableService());


            _tableServices[Tables.Street].OnEdit += (dataRow) => { OnEditStreet?.Invoke(dataRow); };
            _tableServices[Tables.Street].OnAdd += () => { OnAddStreet?.Invoke(); };

            _tableServices[Tables.Districts].OnEdit += (dataRow) => { OnEditDistrict?.Invoke(dataRow); };
            _tableServices[Tables.Districts].OnAdd += () => { OnAddDistrict?.Invoke(); };

            _tableServices[Tables.InventoryDirectories].OnEdit += (dataRow) => { OnEditInventoryDirectory?.Invoke(dataRow); };
            _tableServices[Tables.InventoryDirectories].OnAdd += () => { OnAddInventoryDirectory?.Invoke(); };

            _tableServices[Tables.UsersAbilities].OnEdit += (dataRow) => { OnEditUserAbilities?.Invoke(dataRow); };
            _tableServices[Tables.UsersAbilities].OnAdd += () => { OnAddUserAbilities?.Invoke(); };
        }

        public void UpdateTable()
        {
            Table = _currentTable.Read();
        }

        public ICommand AboutProgram
        {
            get
            {
                return new DelegateCommand(() =>
                {
                   OnAboutProgram?.Invoke();
                });

            }
        }
        public ICommand MenuContents
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnMenuContents?.Invoke();
                });

            }
        }

        public ICommand UsersAbilities
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    IUserAbilitiesData abilities = _abilitiesService.GetUserAbilitiesByFuncName("UsersAbilities");
                    W = abilities.W;
                    E = abilities.E;
                    D = abilities.D;
                    SelectedIndex = -1;
                    _currentTable = _tableServices[Tables.UsersAbilities];
                    Table = _currentTable.Read();
                });

            }
        }

        public ICommand ChangePassword
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnChangePassword?.Invoke();
                });

            }
        }

        public ICommand Streets
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    IUserAbilitiesData abilities = _abilitiesService.GetUserAbilitiesByFuncName("Streets");
                    W = abilities.W;
                    E = abilities.E;
                    D = abilities.D;
                    SelectedIndex = -1;
                    _currentTable = _tableServices[Tables.Street];
                    Table = _currentTable.Read();
                });

            }
        }

        public ICommand InventoryDirectory
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    IUserAbilitiesData abilities = _abilitiesService.GetUserAbilitiesByFuncName("InventoryDirectory");
                    W = abilities.W;
                    E = abilities.E;
                    D = abilities.D;
                    SelectedIndex = -1;
                    _currentTable = _tableServices[Tables.InventoryDirectories];
                    Table = _currentTable.Read();
                });

            }
        }

        public ICommand Districts
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    IUserAbilitiesData abilities = _abilitiesService.GetUserAbilitiesByFuncName("Districts");
                    W = abilities.W;
                    E = abilities.E;
                    D = abilities.D;
                    SelectedIndex = -1;
                    _currentTable = _tableServices[Tables.Districts];
                    Table = _currentTable.Read();
                });

            }
        }

        public ICommand Delete
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if(_currentTable != null)
                    {
                        if(SelectedIndex >= 0 && SelectedIndex< _table.Rows.Count)
                        {
                            _currentTable.Delete(SelectedIndex);
                            Table = _currentTable.Read();
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
                    if (_currentTable != null)
                        Table = _currentTable.FindAll(FindName);
                });

            }
        }

        public ICommand Edit
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (_currentTable != null)
                    {
                        if (SelectedIndex >= 0 && SelectedIndex < _table.Rows.Count)
                        {
                            _currentTable.Edit(SelectedIndex);
                        }
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
                    if (_currentTable != null)
                    {
                        _currentTable.Add();
                    }
                });

            }
        }

    }
}
