using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase.Entity.UserAbilities;
using WpfDormitories.DataBase;
using WpfTest.ViewModel;
using WpfDormitories.DataBase.Entity.Dorm;

namespace WpfDormitories.ViewModel.DormsVM
{
    public class AddDormViewModel : BasicAddOrEditDormsVM
    {
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedDistrictIndex == -1 || SelectedStreetIndex == -1)
                    {
                        MessageBox.Show("Заполните все поля");
                        return;
                    }
                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataManager.GetInstance().DormsRepository.
                        Create(new DormData(_streets[SelectedStreetIndex].Id, _districts[SelectedDistrictIndex].Id,
                        DormNumber,HouseNumber,NumberRooms,NumberPlace));
                    }
                });
            }
        }
    }
}
