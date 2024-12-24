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
using WpfDormitories.DataBase.Entity.Dorm;

namespace WpfDormitories.Model.Services.Tables
{
    public class InventoryDirectoryTableService : ITableService
    {
        private IList<IInventoryDirectoryData> _inventoryDirectoriesData;

        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }

        /// <summary>
        /// Вызвать событие для изменения объекта.
        /// </summary>
        /// <param name="index"></param>
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IInventoryDirectoryData>(_inventoryDirectoriesData).Rows[index]);
        }

        /// <summary>
        /// Найти объекты по тексту.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public DataTable FindAll(string text)
        {
            _inventoryDirectoriesData = DataManager.GetInstance().InventoryDirectoryRepository.Read();
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

        /// <summary>
        /// Прочитать все объекты.
        /// </summary>
        /// <returns></returns>
        public DataTable Read()
        {
            _inventoryDirectoriesData = DataManager.GetInstance().InventoryDirectoryRepository.Read();
            DataTable dt = DataTableParser.ToDataTable<IInventoryDirectoryData>(_inventoryDirectoriesData);
            dt.Columns.Remove(dt.Columns[0]);
            return dt;
        }

        /// <summary>
        /// Вызвать событие для добавления объекта.
        /// </summary>
        /// <param></param>
        public void Add()
        {
            OnAdd.Invoke();
        }

        /// <summary>
        /// Удалить объект по индексу.
        /// </summary>
        /// <param name="index"></param>
        public void Delete(int index)
        {
            DataManager.GetInstance().InventoryDirectoryRepository.Delete(_inventoryDirectoriesData[index]);
            _inventoryDirectoriesData.Remove(_inventoryDirectoriesData[index]);
        }

        /// <summary>
        /// Получить объект по индексу.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable<IInventoryDirectoryData>(_inventoryDirectoriesData).Rows[index];
        }
    }
}
