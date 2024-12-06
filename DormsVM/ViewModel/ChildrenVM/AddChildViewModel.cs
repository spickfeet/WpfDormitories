using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.Model.FullName;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.ChildrenVM
{
    public class AddChildViewModel : BasicAddOrEditChildVM
    {
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Name) || SelectedGenderIndex == -1)
                    {
                        MessageBox.Show("Заполните все поля");
                        return;
                    }
                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataManager.GetInstance().ChildrenRepository.
                        Create(new ChildData(Genders[SelectedGenderIndex], DateOfBirth, new FullName(Surname,Name,Patronymic)));
                    }
                });
            }
        }
    }
}
