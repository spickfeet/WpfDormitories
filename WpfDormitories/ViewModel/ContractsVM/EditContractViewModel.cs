using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Contract;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.DormsVM
{
    public class EditContractViewModel : BasicAddOrEditContractVM
    {
        private uint _id;
        public EditContractViewModel(uint id, string documentNumber, string documentName, string whoGave, DateOnly startAction, string comment)
        {
            _id = id;
            _documentNumber = documentNumber;
            _name = documentName;
            _whoGave = whoGave;
            _startAction = new(startAction,new TimeOnly());
            _comment = comment;
        }
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
                        DataManager.GetInstance().ContractsRepository.Update(new ContractData(DocumentNumber, Name, WhoGave, new DateOnly(StartAction.Year, StartAction.Month, StartAction.Day), Comment));
                    }
                });
            }
        }
    }
}
