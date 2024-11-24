using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;
using WpfDormitories.DataBase.Entity.District;

namespace WpfDormitories.Model.Services.Tables
{
    public class DistrictsTableService : ITableService
    {
        private IList<IDistrictData> _districtsData;

        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IDistrictData>(_districtsData).Rows[index]);
        }
        public DataTable FindAll(string text)
        {
            _districtsData = DataManager.GetInstance().DistrictsRepository.Read();
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }

            IList<IDistrictData> res = new List<IDistrictData>();
            foreach (IDistrictData streetData in _districtsData)
            {
                if (streetData.Name.ToUpper().Contains(text.ToUpper()))
                {
                    res.Add(streetData);
                }
            }
            _districtsData = res;
            DataTable dt = DataTableParser.ToDataTable<IDistrictData>(_districtsData);
            dt.Columns.Remove(dt.Columns[0]);
            return dt;
        }

        public DataTable Read()
        {
            _districtsData = DataManager.GetInstance().DistrictsRepository.Read();
            DataTable dt = DataTableParser.ToDataTable<IDistrictData>(_districtsData);
            dt.Columns.Remove(dt.Columns[0]);
            return dt;
        }

        public void Add()
        {
            OnAdd.Invoke();
        }

        public void Delete(int index)
        {
            DataManager.GetInstance().DistrictsRepository.Delete(_districtsData[index]);
            _districtsData.Remove(_districtsData[index]);
        }
    }
}
