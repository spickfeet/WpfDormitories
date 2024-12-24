using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.UserAbilitiesVM
{
    public class EditUserAbilitiesViewModel : BasicUserAbilitiesVM
    {
        private uint _id;
        public EditUserAbilitiesViewModel(uint id,uint userId, uint menuElementId,bool r, bool w, bool e, bool d)
        {
            _id = id;
            _selectedUserIndex = _users.ToList().FindIndex(item => item.Id == userId);
            _selectedMenuElementIndex = _elementsMenu.ToList().FindIndex(item => item.Id == menuElementId);
            _r = r;
            _w = w;
            _e = e;
            _d = d;
        }

        /// <summary>
        /// Изменить данные о правах пользователя.
        /// </summary>
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataManager.GetInstance().UsersAbilitiesRepository.Update(new UserAbilitiesData(_id, _users[SelectedUserIndex].Id,
                        _elementsMenu[SelectedMenuElementIndex].Id, R, W, E, D));
                    }
                });
            }
        }
    }
}
