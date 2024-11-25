﻿using System;
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
    public class AddInventoryViewModel : BasicAddOrEditInventoryVM
    {
        public AddInventoryViewModel(uint roomId)
        {
            _roomId = roomId;
        }
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
                        DataManager.GetInstance().InventoryRepository.
                        Create(new InventoryData(_roomId, _inventoryDirectory[SelectedIndex].Id));
                    }
                });
            }
        }
    }
}