using System.Data;
using System.Windows.Input;
using WpfDormitories.Model.Services.Tables;
using WpfDormitories.Model.Services.Tables.ExportFormattedTables;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel
{
    public class ExportViewModel : BasicVM
    {
        private DataTable _table;
        private ITableService _tableService;

        public DataTable Table 
        {
            get {  return _table; }
            set { Set(ref _table, value); }
        }

        public ExportViewModel(ITableService tableService)
        {
            _tableService = tableService;
            Table = _tableService.Read();
        }

        public ICommand ExportToWord
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MyExportToWord.ExportTable(Table);
                });
            }
        }
        public ICommand ExportToExcel
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Model.Services.Tables.ExportToExcel.ExportTable(Table);
                });
            }
        }
    }
}
