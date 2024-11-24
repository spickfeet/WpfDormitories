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
    public class EditDistrictViewModel : BasicDirectoryVM
    {
        private uint _id;
        public EditDistrictViewModel(uint id, string name)
        {
            _id = id;
            _name = name;
        }
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataManager.GetInstance().DistrictsRepository.Update(new DistrictData(_id, Name));
                    }
                });
            }
        }
    }
}
