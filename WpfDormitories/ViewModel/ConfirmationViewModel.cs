using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel
{
    public class ConfirmationViewModel
    {
        public Action OnConfirmed;
        public bool Result { get; set; }

        public ConfirmationViewModel()
        {
            Result = false;
        }

        public ICommand Yes
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Result = true;
                    OnConfirmed.Invoke();
                });
            }
        }
        public ICommand No
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    OnConfirmed.Invoke();
                });
            }
        }
    }
}
