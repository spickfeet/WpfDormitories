using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;

namespace WpfDormitories.Model.Services.Tables
{
    internal class StreetsTableService : ITableService
    {
        private ICollection<IStreetData> _streetsData;

        public Action<DataRow> OnEdit { get; set; }
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IStreetData>(_streetsData).Rows[index]);
        }


        public DataTable FindAll(string elementName)
        {
            throw new NotImplementedException();
        }

        public DataTable Read()
        {
            _streetsData = DataManager.GetInstance().StreetsRepository.Read();
            DataTable dt = DataTableParser.ToDataTable<IStreetData>(_streetsData);
            dt.Columns.Remove(dt.Columns[0]);
            return dt;
        }

        public void Add()
        {
            
        }

        public void Delete(int index)
        {
            DataManager.GetInstance().StreetsRepository.Delete(_streetsData[index]);
        }
    }
}
