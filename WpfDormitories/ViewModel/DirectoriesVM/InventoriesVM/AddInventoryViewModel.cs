using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Inventory.InventoryDirectory;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.DirectoriesVM.InventoriesVM
{
    public class AddInventoryViewModel : BasicDirectoryVM
    {
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DataManager.GetInstance().InventoryDirectoryRepository.Create(new InventoryDirectoryData(Name));
                    OnApply?.Invoke();
                });
            }
        }
    }
}
