using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel
{
    class QueryViewModel : BasicVM
    {
        private string _query;
        private DataTable _dataBaseTable;
        public DataTable DataBaseTable 
        { 
            get {  return _dataBaseTable; }
            set 
            {
                Set<DataTable>(ref _dataBaseTable, value);
            }
        }
        public string Query 
        {
            get {  return _query; }
            set
            {
                Set<string>(ref _query, value);
            }
        }

        public void ExecuteQuery()
        {
            try
            {
                string exception = string.Empty;
                DataTable table = DormitorySQLConnection.GetInstance().GetData(
                    Query
                );
                DataBaseTable = table;
            }
            catch (Exception ex) 
            {
                DataBaseTable.Clear();
                MessageBox.Show(ex.Message);
            }
        }

        public ICommand ClickExecuteQuery
        {
            get
            {
                return new DelegateCommand(() => ExecuteQuery());
            }
        }
    }
}
