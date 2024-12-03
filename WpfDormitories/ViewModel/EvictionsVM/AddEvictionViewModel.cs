using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Resident;
using WpfTest.ViewModel;
using WpfDormitories.Model.Services.Tables;
using System.Data;
using WpfDormitories.DataBase.Entity.Eviction;
using WpfDormitories.Model.PersonDocument;

namespace WpfDormitories.ViewModel.EvictionsVM
{
    public class AddEvictionViewModel : BasicVM, IApplicableVM
    {
        private ITableService _tableService;

        private DateTime _date;
        private string _reason;

        private DataTable _residents;
        private int _selectedResidentIndex;

        private string _findText;

        public DateTime Date
        {
            get { return _date; } 
            set { Set(ref _date, value); }
        }
        public string Reason
        {
            get { return _reason; }
            set { Set(ref _reason, value); }
        }

        public DataTable Residents
        {
            get { return _residents; }
            set { Set(ref _residents, value); }
        }
        public int SelectedResidentIndex
        {
            get { return _selectedResidentIndex; }
            set { Set(ref _selectedResidentIndex, value); }
        }

        public string FindText
        {
            get { return _findText; }
            set { Set(ref _findText, value); }
        }

        public AddEvictionViewModel()
        {
            _date = DateTime.Now;
            ConfirmApplyStatus = false;
            _selectedResidentIndex = -1;
            _tableService = new ResidentsTableService();
            _residents = _tableService.Read();
        }

        public ICommand Find
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Residents = _tableService.FindAll(FindText);
                });
            }
        }

        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (SelectedResidentIndex == -1)
                    {
                        MessageBox.Show("Выберите кого выселить");
                        return;
                    }
                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataRow row = _tableService.GetByIndex(SelectedResidentIndex);
                        DataManager.GetInstance().EvictionsRepository.
                        Create(new EvictionData((IPersonDocuments)row[3],Reason,Date));
                        _tableService.Delete(SelectedResidentIndex);
                    }
                });
            }
        }
        public bool ConfirmApplyStatus { get; set; }
        public Action OnApply { get; set; }
    }
}
