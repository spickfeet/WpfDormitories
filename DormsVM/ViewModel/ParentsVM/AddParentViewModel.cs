using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Child.ParentsAndChildren;
using WpfDormitories.Model.Services.Tables;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.ParentsVM
{
    public class AddParentViewModel : BasicAddOrEdictParentVM
    {
        public AddParentViewModel(uint childId)
        {
            _childId = childId;
            ConfirmApplyStatus = false;
            _selectedParentIndex = -1;
            _parentsTable = _parentsTableService.Read();
        }

        public ICommand Find
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ParentsTable = _parentsTableService.FindAll(FindText);
                    _selectedParentIndex = -1;
                });
            }
        }

        /// <summary>
        /// Добавить данные о родителе.
        /// </summary>
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedParentIndex == -1)
                    {
                        MessageBox.Show("Выберите родителя");
                        return;
                    }
                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataRow row = _parentsTableService.GetByIndex(SelectedParentIndex);
                        DataManager.GetInstance().ParentsAndChildrenRepository.
                        Create(new ParentsAndChildrenData(_parentsTableService.Parents[SelectedParentIndex].Id,_childId));
                    }
                });
            }
        }
    }
}
