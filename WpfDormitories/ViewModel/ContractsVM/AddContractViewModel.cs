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
using System.Diagnostics.Contracts;
using WpfDormitories.DataBase.Entity.Contract;

namespace WpfDormitories.ViewModel.DormsVM
{
    public class AddContractViewModel : BasicAddOrEditContractVM
    {
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (string.IsNullOrEmpty(DocumentNumber) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(WhoGave))
                    {
                        MessageBox.Show("Заполните все поля");
                        return;
                    }
                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataManager.GetInstance().ContractsRepository.
                        Create(new ContractData(DocumentNumber, Name, WhoGave, new DateOnly(StartAction.Year, StartAction.Month, StartAction.Day), Comment));
                    }
                });
            }
        }
    }
}
