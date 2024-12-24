using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.Model.FullName;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.ChildrenVM
{
    public class EditChildViewModel : BasicAddOrEditChildVM
    {
        private uint _id;
        public EditChildViewModel(uint id, string gender, DateTime date, IFullName fullName)
        {
            _id = id;
            _selectedGenderIndex = gender == "М" ? 0 : 1;
            _dateOfBirth = date;
            _surname = fullName.Surname;
            _name = fullName.Name;
            _patronymic = fullName.Patronymic;
        }
        
        /// <summary>
        /// Изменить данные о ребенке.
        /// </summary>
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
                        DataManager.GetInstance().ChildrenRepository.Update(new ChildData(_id, Genders[SelectedGenderIndex], DateOfBirth, new FullName(Surname, Name, Patronymic)));
                    }
                });
            }
        }
    }
}
