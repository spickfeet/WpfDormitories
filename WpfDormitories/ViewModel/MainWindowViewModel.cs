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
using WpfDormitories.Model.Services.Tables.ExportFormattedTables;
using WpfDormitories.TemporarySolutions;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel
{
    public class MainWindowViewModel : BasicVM
    {
        private DataTable _table;
        private Visibility _w;
        private Visibility _e;
        private Visibility _d;
        private int _selectedIndex;
        private string _findText;

        private UserAbilitiesService _abilitiesService;

        public Action<ITableService> OnExport;

        public Action OnMenuContents;
        public Action OnAboutProgram;
        public Action OnChangePassword;
        public Action OnCustomQuery;

        public Action<IUserAbilitiesData> OnDorms;
        public Action<IUserAbilitiesData> OnContracts;
        public Action<IUserAbilitiesData> OnEvictions;
        public Action<IUserAbilitiesData> OnChildren;

        private ITableService _currentTable;
        private IDictionary<Tables,ITableService> _tableServices;

        public Action OnDelete;

        public Action<DataRow> OnEditStreet;
        public Action OnAddStreet;

        public Action<DataRow> OnEditDistrict;
        public Action OnAddDistrict;

        public Action<DataRow> OnEditInventoryDirectory;
        public Action OnAddInventoryDirectory;

        public Action<DataRow> OnEditUserAbilities;
        public Action OnAddUserAbilities;

        public bool DeleteConfirmStatus {  get; set; }

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

        public string FindText
        {
            get { return _findText; }
            set
            {
                Set(ref _findText, value);
                if (_currentTable != null)
                    Table = _currentTable.FindAll(FindText);
            }
        }

        public Visibility W
        {
            get { return _w; }
            set
            {
                Set<Visibility>(ref _w, value);
            }
        }

        public Visibility E
        {
            get { return _e; }
            set
            {
                Set<Visibility>(ref _e, value);
            }
        }

        public Visibility D
        {
            get { return _d; }
            set
            {
                Set<Visibility>(ref _d, value);
            }
        }


        public MainWindowViewModel()
        {
            DeleteConfirmStatus = false;

            _w = Visibility.Collapsed;
            _e = Visibility.Collapsed;
            _d = Visibility.Collapsed;
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

        public ICommand Children
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    IUserAbilitiesData abilities = _abilitiesService.GetUserAbilitiesByFuncName("Children");
                    OnChildren?.Invoke(abilities);
                });

            }
        }

        public ICommand Contracts
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    IUserAbilitiesData abilities = _abilitiesService.GetUserAbilitiesByFuncName("Contracts");
                    OnContracts?.Invoke(abilities);
                });

            }
        }

        public ICommand Evictions
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    IUserAbilitiesData abilities = _abilitiesService.GetUserAbilitiesByFuncName("Evictions");
                    OnEvictions?.Invoke(abilities);
                });

            }
        }

        public ICommand Dorms
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    IUserAbilitiesData abilities = _abilitiesService.GetUserAbilitiesByFuncName("Dorms");
                    OnDorms?.Invoke(abilities);
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
                    W = abilities.W == true ? Visibility.Visible : Visibility.Collapsed;
                    E = abilities.E == true ? Visibility.Visible : Visibility.Collapsed;
                    D = abilities.D == true ? Visibility.Visible : Visibility.Collapsed;
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

        public ICommand CustomQuery
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnCustomQuery?.Invoke();
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
                    W = abilities.W == true ? Visibility.Visible : Visibility.Collapsed;
                    E = abilities.E == true ? Visibility.Visible : Visibility.Collapsed;
                    D = abilities.D == true ? Visibility.Visible : Visibility.Collapsed;
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
                    W = abilities.W == true ? Visibility.Visible : Visibility.Collapsed;
                    E = abilities.E == true ? Visibility.Visible : Visibility.Collapsed;
                    D = abilities.D == true ? Visibility.Visible : Visibility.Collapsed;
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
                    W = abilities.W == true ? Visibility.Visible : Visibility.Collapsed;
                    E = abilities.E == true ? Visibility.Visible : Visibility.Collapsed;
                    D = abilities.D == true ? Visibility.Visible : Visibility.Collapsed;
                    SelectedIndex = -1;
                    _currentTable = _tableServices[Tables.Districts];
                    Table = _currentTable.Read();
                });

            }
        }

        public ICommand StreetsExport
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnExport?.Invoke(new StreetsTableService());
                });

            }
        }

        public ICommand DistrictsExport
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnExport?.Invoke(new DistrictsTableService());
                });

            }
        }

        public ICommand InventoryExport
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnExport?.Invoke(new InventoryDirectoryTableService());
                });

            }
        }

        public ICommand DormsExport
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnExport?.Invoke(new DormsTableService());
                });

            }
        }

        public ICommand RoomsExport
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnExport?.Invoke(new ExportRoomsTableService());
                });

            }
        }

        public ICommand ContractsExport
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnExport?.Invoke(new ContractsTableService());
                });

            }
        }

        public ICommand ResidentsExport
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnExport?.Invoke(new ExportResidentsTableService());
                });

            }
        }

        public ICommand ChildrenExport
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnExport?.Invoke(new ChildrenTableService());
                });

            }
        }

        public ICommand EvictionsExport
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnExport?.Invoke(new ExportEvictionsTableService());
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
                            OnDelete?.Invoke();
                            if (DeleteConfirmStatus)
                            {
                                _currentTable.Delete(SelectedIndex);
                                Table = _currentTable.Read();
                                DeleteConfirmStatus = false;
                            }
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
