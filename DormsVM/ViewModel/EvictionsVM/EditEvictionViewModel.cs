using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Eviction;
using WpfDormitories.Model.PersonDocument;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.EvictionsVM
{
    public class EditEvictionViewModel : BasicVM, IApplicableVM
    {
        private uint _id;
        private IPersonDocuments _personDocuments;
        private string _reason;
        private DateTime _date;

        public string Reason
        {
            get { return _reason; }
            set { Set(ref _reason, value); }
        }
        public DateTime Date
        {
            get { return _date; }
            set { Set(ref _date, value); }
        }

        public EditEvictionViewModel(uint id, IPersonDocuments personDocuments, string reason, DateTime date) 
        {
            ConfirmApplyStatus = false;
            _id = id;
            _personDocuments = personDocuments;
            _reason = reason;
            _date = date;
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
                        DataManager.GetInstance().EvictionsRepository.Update(new EvictionData(_id, _personDocuments, Reason, Date));
                    }
                });
            }
        }

        public Action OnApply { get; set; }
        public bool ConfirmApplyStatus { get; set; }
    }
}
