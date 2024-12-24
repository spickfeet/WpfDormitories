using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Inventory;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.InventoryVM
{
    public class EditInventoryViewModel : BasicAddOrEditInventoryVM
    {
        private uint _id;
        public EditInventoryViewModel(uint id, uint roomId, uint nameId)
        {
            _id = id;
            _roomId = roomId;
            _selectedIndex = _inventoryDirectory.FindIndex(item => item.Id == nameId);
        }

        /// <summary>
        /// Изменить данные инвентаря.
        /// </summary>
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (_selectedIndex == -1)
                    {
                        MessageBox.Show("Выберите инвентарь");
                        return;
                    }
                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataManager.GetInstance().InventoryRepository.Update(new InventoryData(_id, _roomId, _inventoryDirectory[SelectedIndex].Id));
                    }
                });
            }
        }
    }
}
