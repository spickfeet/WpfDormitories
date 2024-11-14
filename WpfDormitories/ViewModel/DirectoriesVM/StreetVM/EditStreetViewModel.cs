﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Street;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.DirectoriesVM.StreetVM
{
    public class EditStreetViewModel : BasicDirectoryVM
    {
        private uint _id;
        public EditStreetViewModel(uint id, string name)
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
                    DataManager.GetInstance().StreetsRepository.Update(new StreetData(_id, Name));
                    OnApply?.Invoke();
                });
            }
        }
    }
}