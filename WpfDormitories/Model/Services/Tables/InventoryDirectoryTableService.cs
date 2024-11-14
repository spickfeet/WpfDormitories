using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;
using WpfDormitories.DataBase.Entity.Inventory.InventoryDirectory;

namespace WpfDormitories.Model.Services.Tables
{
    public class InventoryDirectoryTableService : ITableService
    {
        private IList<IInventoryDirectoryData> _inventoryDirectoriesData;

        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IInventoryDirectoryData>(_inventoryDirectoriesData).Rows[index]);
        }
        public DataTable FindAll(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }

            IList<IInventoryDirectoryData> res = new List<IInventoryDirectoryData>();
            foreach (IInventoryDirectoryData inventoryDirectoryData in _inventoryDirectoriesData)
            {
                if (inventoryDirectoryData.Name.ToUpper().Contains(text.ToUpper()))
                {
                    res.Add(inventoryDirectoryData);
                }
            }
            _inventoryDirectoriesData = res;
            DataTable dt = DataTableParser.ToDataTable<IInventoryDirectoryData>(res);
            dt.Columns.Remove(dt.Columns[0]);
            return dt;
        }

        public DataTable Read()
        {
            _inventoryDirectoriesData = DataManager.GetInstance().InventoryDirectoryRepository.Read();
            DataTable dt = DataTableParser.ToDataTable<IInventoryDirectoryData>(_inventoryDirectoriesData);
            dt.Columns.Remove(dt.Columns[0]);
            return dt;
        }

        public void Add()
        {
            OnAdd.Invoke();
        }

        public void Delete(int index)
        {
            DataManager.GetInstance().InventoryDirectoryRepository.Delete(_inventoryDirectoriesData[index]);
        }
    }
}
