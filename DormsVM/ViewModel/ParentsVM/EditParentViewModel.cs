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
    public class EditParentViewModel : BasicAddOrEdictParentVM
    {
        private uint _id;
        public EditParentViewModel(uint id, uint parentId, uint childId)
        {
            _id = id;
            _childId = childId;

            _parentsTable = _parentsTableService.Read();

            SelectedParentIndex = _parentsTableService.Parents.FindIndex(item => item.Id == parentId);

            ConfirmApplyStatus = false;
        }

        public ICommand Find
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ParentsTable = _parentsTableService.FindAll(FindText);
                });
            }
        }

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
                        Update(new ParentsAndChildrenData(_id, _parentsTableService.Parents[SelectedParentIndex].Id, _childId));
                    }
                });
            }
        }
    }
}
