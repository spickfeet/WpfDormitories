using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.District;
using WpfDormitories.DataBase.Entity.Inventory.InventoryDirectory;

namespace WpfDormitories.ViewModel.InventoryVM
{
    public abstract class BasicAddOrEditInventoryVM : BasicVM, IApplicableVM
    {
        protected uint _roomId;
        protected int _selectedIndex;
        protected List<IInventoryDirectoryData> _inventoryDirectory;

        public BasicAddOrEditInventoryVM()
        {
            _selectedIndex = -1;
            _inventoryDirectory = DataManager.GetInstance().InventoryDirectoryRepository.Read().ToList<IInventoryDirectoryData>();
        }
        public Action OnApply { get; set; }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { Set(ref _selectedIndex, value); }
        }

        public IList<string> InventoryDirectory
        {
            get 
            {
                IList<string> res = new List<string>();
                foreach (IInventoryDirectoryData inventoryDirectory in _inventoryDirectory)
                {
                    res.Add(inventoryDirectory.Name);
                }
                return res;
            }
        }
        public bool ConfirmApplyStatus { get; set; }
    }
}
