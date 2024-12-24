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
    public class AddDormViewModel : BasicAddOrEditDormVM
    {

        /// <summary>
        /// Добавить данные об общежитии.
        /// </summary>
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedDistrictIndex == -1 || SelectedStreetIndex == -1 || string.IsNullOrEmpty(DormNumber) || string.IsNullOrEmpty(HouseNumber) || NumberRooms == 0 || NumberPlace == 0)
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
