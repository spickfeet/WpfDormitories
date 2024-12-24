using System.Data;
using System.Windows.Input;
using WpfDormitories.DataBase.Entity.Contract;
using WpfDormitories.Model.Services.Tables;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel
{
    public class ExportViewModel : BasicVM
    {
        private DataTable _table;
        private ITableService _tableService;
        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { Set(ref _selectedIndex, value); }
        }

        public DataTable Table 
        {
            get {  return _table; }
            set { Set(ref _table, value); }
        }

        public ExportViewModel(ITableService tableService)
        {
            _selectedIndex = -1;
            _tableService = tableService;
            Table = _tableService.Read();
        }

        /// <summary>
        /// Отправить данные в Word.
        /// </summary>
        public ICommand ExportToWord
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if(_tableService is ContractsTableService c)
                    {
                        if(SelectedIndex != -1)
                        {
                            DataRow dr = _tableService.GetByIndex(SelectedIndex);
                            DormsModel.Model.ExportToWord.ExportContract(new ContractData((uint)dr[0], (string)dr[1], (string)dr[2], (string)dr[3], (DateTime)dr[4], (string)dr[5]));
                        }
                        return;
                    }
                    DormsModel.Model.ExportToWord.ExportTable(Table);
                });
            }
        }

        /// <summary>
        /// Отправить данные в Excel.
        /// </summary>
        public ICommand ExportToExcel
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DormsModel.Model.ExportToExcel.ExportTable(Table);
                });
            }
        }
    }
}
