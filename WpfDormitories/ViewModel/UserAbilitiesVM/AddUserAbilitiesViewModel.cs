using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.MenuElement;
using WpfDormitories.DataBase.Entity.User;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.UserAbilitiesVM
{
    public class AddUserAbilitiesViewModel : BasicUserAbilitiesVM
    {
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if(SelectedMenuElementIndex == -1 || SelectedUserIndex == -1)
                    {
                        MessageBox.Show("Пользователь или элемент меню не выбран");
                        return;
                    }
                    DataManager.GetInstance().UsersAbilitiesRepository.
                    Create(new UserAbilitiesData(_users[SelectedUserIndex].Id,
                    _elementsMenu[SelectedMenuElementIndex].Id,R,W,E,D));
                    OnApply?.Invoke();
                });
            }
        }
    }
}
