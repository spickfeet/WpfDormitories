using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.District;
using WpfDormitories.DataBase.Entity.Street;

namespace WpfDormitories.ViewModel.DormsVM
{
    public abstract class BasicAddOrEditContractVM : BasicVM, IApplicableVM
    {
        protected string _documentNumber;
        protected string _name;
        protected string _whoGave;
        protected DateTime _startAction;
        protected string _comment;

        public string DocumentNumber
        {
            get { return _documentNumber; }
            set { Set(ref _documentNumber, value); }
        }
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }
        public string WhoGave
        {
            get { return _whoGave; }
            set { Set(ref _whoGave, value); }
        }
        public DateTime StartAction
        {
            get { return _startAction; }
            set { Set(ref _startAction, value); }
        }
        public string Comment
        {
            get { return _comment; }
            set { Set(ref _comment, value); }
        }

        public Action OnApply { get; set; }
        public bool ConfirmApplyStatus { get; set; }

        public BasicAddOrEditContractVM()
        {
            _startAction = DateTime.Now;
        }
    }
}
