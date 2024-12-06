using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.Model.Services.Tables;

namespace WpfDormitories.ViewModel.ParentsVM
{
    public abstract class BasicAddOrEdictParentVM : BasicVM, IApplicableVM
    {
        protected uint _childId;
        protected string _findText;
        protected int _selectedParentIndex;
        protected DataTable _parentsTable;
        protected ParentsTableService _parentsTableService;

        private DataRow _selectedItem;

        public string FindText
        {
            get { return _findText; }
            set 
            { 
                Set(ref _findText, value);
                ParentsTable = _parentsTableService.FindAll(value);
            }
        }

        public int SelectedParentIndex
        {
            get { return _selectedParentIndex; }
            set { Set(ref _selectedParentIndex, value); }
        }
        public DataTable ParentsTable
        {
            get { return _parentsTable; }
            set { Set(ref _parentsTable, value); }
        }

        public Action OnApply { get; set; }
        public bool ConfirmApplyStatus { get; set; }

        public BasicAddOrEdictParentVM()
        {
            _parentsTableService = new();
        }
    }
}
