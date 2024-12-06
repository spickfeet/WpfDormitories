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
        public void Edit(int index)
        {
            OnEdit.Invoke(DataTableParser.ToDataTable<IInventoryData>(_inventoryData).Rows[index]);
        }
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

        public void Add()
        {
            OnAdd.Invoke();
        }

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

        public DataRow GetByIndex(int index)
        {
            return DataTableParser.ToDataTable<IInventoryData>(_inventoryData).Rows[index];
        }
    }
}
