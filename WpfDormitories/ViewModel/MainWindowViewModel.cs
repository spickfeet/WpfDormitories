using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.District;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.TemporarySolutions;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel
{
    public class MainWindowViewModel : BasicVM
    {
        private DataTable _table;
        public DataTable Table
        {
            get { return _table; }
            set
            {
                Set<DataTable>(ref _table, value);
            }
        }

        public ICommand Streets
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DataTable dt = DataTableParser.ToDataTable<IStreetData>(DataManager.GetInstance().StreetsRepository.Read());
                    dt.Columns.Remove(dt.Columns[0]);
                    Table = dt;
                });

            }
        }

        public ICommand Districts
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DataTable dt = DataTableParser.ToDataTable<IDistrictData>(DataManager.GetInstance().DistrictsRepository.Read());
                    dt.Columns.Remove(dt.Columns[0]);
                    Table = dt;
                });

            }
        }
    }
}
