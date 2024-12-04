using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDormitories.ViewModel.ChildrenVM
{
    public class BasicAddOrEditChildVM : BasicVM, IApplicableVM
    {
        protected string[] _genders;
        protected int _selectedGenderIndex;
        protected DateTime _dateOfBirth;
        protected string _surname;
        protected string _name;
        protected string _patronymic;
        public Action OnApply { get; set; }
        public bool ConfirmApplyStatus { get; set; }
        public string[] Genders
        {
            get { return _genders; }
            set { Set(ref _genders, value); }
        }
        public int SelectedGenderIndex
        {
            get { return _selectedGenderIndex; }
            set { Set(ref _selectedGenderIndex, value); }
        }
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { Set(ref _dateOfBirth, value); }
        }
        public string Surname
        {
            get { return _surname; }
            set { Set(ref _surname, value); }
        }
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }
        public string Patronymic
        {
            get { return _patronymic; }
            set { Set(ref _patronymic, value); }
        }

        public BasicAddOrEditChildVM()
        {
            _genders = ["М", "Ж"];
            _selectedGenderIndex = -1;
            DateOfBirth = DateTime.Now;
        }
    }
}
