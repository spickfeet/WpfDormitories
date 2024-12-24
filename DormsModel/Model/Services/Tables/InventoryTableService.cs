using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Inventory.InventoryDirectory;
using WpfDormitories.DataBase;
using WpfDormitories.TemporarySolutions;
using WpfDormitories.DataBase.Entity.Inventory;
using WpfDormitories.DataBase.Entity.Room;

namespace WpfDormitories.Model.Services.Tables
{
    public class InventoryTableService : ITableService
    {
        private uint _roomId;
        private List<IInventoryData> _inventoryData;
        private List<IInventoryDirectoryData> _inventoryDirectories;

        public Action<DataRow> OnEdit { get; set; }
        public Action OnAdd { get; set; }

        public InventoryTableService(uint roomId)
        {
            _roomId = roomId;
            _inventoryDirectories = DataManager.GetInstance().InventoryDirectoryRepository.Read().ToList();
        }

        /// <summary>
        /// Вызвать событие для изменения объекта.
        /// </summary>
        /// <param name="index"></param>
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IInventoryData>(_inventoryData).Rows[index]);
        }

        /// <summary>
        /// Найти объекты по тексту.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public DataTable FindAll(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Read();
            }

            List<IInventoryData> allInventory = DataManager.GetInstance().InventoryRepository.Read().ToList();
            _inventoryData = new();
            foreach (IInventoryData inventory in allInventory)
            {
                if (inventory.RoomId == _roomId)
                {
                    _inventoryData.Add(inventory);
                }
            }

            List<IInventoryData> res = new();

            foreach (IInventoryData inventory in _inventoryData)
            {
                if (_inventoryDirectories.Find(item => item.Id == inventory.NameId).Name.ToUpper().Contains(text.ToUpper()))
                {
                    res.Add(inventory);
                }
            }
            _inventoryData = res;
            DataTable dt = CreateDataTable();
            foreach (IInventoryData inventory in _inventoryData)
            {
                dt.Rows.Add(inventory.Id, _inventoryDirectories.Find(item => item.Id == inventory.NameId).Name);
            }
            return dt;
        }

        /// <summary>
        /// Прочитать все объекты.
        /// </summary>
        /// <returns></returns>
        public DataTable Read()
        {
            List <IInventoryData> allInventory = DataManager.GetInstance().InventoryRepository.Read().ToList();
            _inventoryData = new();
            foreach(IInventoryData inventory in allInventory)
            {
                if(inventory.RoomId == _roomId)
                {
                    _inventoryData.Add(inventory);
                }
            }
            DataTable dt = CreateDataTable();
            foreach (IInventoryData inventory in _inventoryData)
            {
                dt.Rows.Add(inventory.Id, _inventoryDirectories.Find(item => item.Id == inventory.NameId).Name);
            }
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
            DataManager.GetInstance().InventoryRepository.Delete(_inventoryData[index]);
            _inventoryData.Remove(_inventoryData[index]);
        }

        private DataTable CreateDataTable()
        {
            DataTable res = new();
            res.Columns.Add("ID");
            res.Columns.Add("Инвентарь");
            return res;
        }

        /// <summary>
        /// Получить объект по индексу.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable<IInventoryData>(_inventoryData).Rows[index];
        }
    }
}
