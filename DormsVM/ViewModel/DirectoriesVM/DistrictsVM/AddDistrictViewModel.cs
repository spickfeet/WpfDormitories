using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.District;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.DirectoriesVM.DistrictsVM
{
    public class AddDistrictViewModel : BasicDirectoryVM
    {
        /// <summary>
        /// Добавить данные о районе.
        /// </summary>
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
                        DataManager.GetInstance().DistrictsRepository.Create(new DistrictData(Name));
                    }
                });
            }
        }
    }
}
