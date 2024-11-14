using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Street;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.DirectoriesVM.StreetVM
{
    public class AddStreetViewModel : BasicDirectoryVM
    {
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DataManager.GetInstance().StreetsRepository.Create(new StreetData(Name));
                    OnApply?.Invoke();
                });
            }
        }
    }
}
