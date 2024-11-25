﻿using System;
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
    public class EditInventoryViewModel : BasicDirectoryVM
    {
        private uint _id;
        public EditInventoryViewModel(uint id, string name)
        {
            _id = id;
            _name = name;
        }
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
                        DataManager.GetInstance().InventoryDirectoryRepository.Update(new InventoryDirectoryData(_id, Name));
                    }
                });
            }
        }
    }
}
