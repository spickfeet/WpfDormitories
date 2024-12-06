using System;
using System.Collections;
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
    public class QueryViewModel : BasicVM
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
                if(IsGetQuery())
                    DataBaseTable = DormitorySQLConnection.GetInstance().GetData(Query);
                else
                {
                    DormitorySQLConnection.GetInstance().Request(Query);
                    MessageBox.Show("Запрос выполнен");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsGetQuery()
        {
            string command = Query.Split(' ')[0].ToUpper();
            if (command == "SELECT")
            {
                return true;
            }
            else
            {
                return false;
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
