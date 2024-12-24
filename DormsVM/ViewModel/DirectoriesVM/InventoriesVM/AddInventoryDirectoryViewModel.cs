using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Inventory.InventoryDirectory;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.DirectoriesVM.InventoriesVM
{
    public class AddInventoryDirectoryViewModel : BasicDirectoryVM
    {
        /// <summary>
        /// Добавить данные о справочнике инвентаря.
        /// </summary>
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        MessageBox.Show("Заполните все поля");
                        return;
                    }
                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataManager.GetInstance().InventoryDirectoryRepository.Create(new InventoryDirectoryData(Name));
                    }
                });
            }
        }
    }
}
