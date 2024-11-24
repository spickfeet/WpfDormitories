using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.District;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.DirectoriesVM.DistrictsVM
{
    public class AddDistrictViewModel : BasicDirectoryVM
    {
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
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
