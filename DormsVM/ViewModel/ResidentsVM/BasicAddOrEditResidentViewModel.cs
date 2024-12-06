using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase.Entity.Street;

namespace WpfDormitories.ViewModel.ResidentsVM
{
    public abstract class BasicAddOrEditResidentViewModel : BasicVM, IApplicableVM
    {
        protected uint _contractId;

        protected List<IStreetData> _streets;
        protected List<IDormData> _dorms;
        protected int _selectedIndexDorm;

        protected List<IRoomData> _roomsData;
        protected List<string> _rooms;
        protected int _selectedIndexRoom;

        protected string _surname;
        protected string _name;
        protected string _patronymic;
        protected int _selectedIndexGender;
        protected string[] _genders;
        protected bool _haveChildren;

        protected string _registrationNumber;
        protected string _seriesPassport;
        protected string _numberPassport;
        protected string _placeOfWork;
        protected float _payment;

        protected DateTime _dateOfIssue;
        protected string _whoGave;
        protected DateTime _dateOfBirth;
        protected string _placeOfStudy;
        protected DateTime _arrivalDate;

        public List<string> Dorms
        {
            get 
            {
                List<string> res = new();
                foreach (IDormData dorm in _dorms)
                {
                    res.Add($"Общежитие №{dorm.DormNumber} {_streets.Find(item => item.Id == dorm.StreetId).Name}");
                }
                return res;
            }
        }

        public virtual int SelectedIndexDorm
        {
            get { return _selectedIndexDorm; }
            set 
            {
                Set(ref _selectedIndexDorm, value);
                SelectedIndexRoom = -1;
                _roomsData = DataManager.GetInstance().RoomsRepository.Read().ToList().
                    FindAll(item  => (item.DormId == _dorms[SelectedIndexDorm].Id && item.NumberFreePlaces > 0));
                List<string> rooms = new();
                foreach (IRoomData room in _roomsData)
                    rooms.Add($"Комната №{room.NumberRoom}\nСвободных мест: {room.NumberFreePlaces} из {room.TotalNumberPlaces}");
                Rooms = rooms;
            }
        }

        public List<string> Rooms
        {
            get { return _rooms; }
            set { Set(ref _rooms, value); }
        }

        public int SelectedIndexRoom
        {
            get { return _selectedIndexRoom; }
            set { Set(ref _selectedIndexRoom, value); }
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

        public int SelectedIndexGender
        {
            get { return _selectedIndexGender; }
            set
            {
                Set(ref _selectedIndexGender, value);;
            }
        }

        public string[] Genders
        {
            get { return _genders; }
            set { Set(ref _genders, value); }
        }

        public bool HaveChildren
        {
            get { return _haveChildren; }
            set { Set(ref _haveChildren, value); }
        }

        public string RegistrationNumber
        {
            get { return _registrationNumber; }
            set { Set(ref _registrationNumber, value); }
        }

        public string SeriesPassport
        {
            get { return _seriesPassport; }
            set { Set(ref _seriesPassport, value); }
        }

        public string NumberPassport
        {
            get { return _numberPassport; }
            set { Set(ref _numberPassport, value); }
        }

        public string PlaceOfWork
        {
            get { return _placeOfWork; }
            set { Set(ref _placeOfWork, value); }
        }

        public float Payment
        {
            get { return _payment; }
            set { Set(ref _payment, value); }
        }

        public DateTime DateOfIssue
        {
            get { return _dateOfIssue; }
            set { Set(ref _dateOfIssue, value); }
        }

        public string WhoGave
        {
            get { return _whoGave; }
            set { Set(ref _whoGave, value); }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { Set(ref _dateOfBirth, value); }
        }

        public string PlaceOfStudy
        {
            get { return _placeOfStudy; }
            set { Set(ref _placeOfStudy, value); }
        }

        public DateTime ArrivalDate
        {
            get { return _arrivalDate; }
            set { Set(ref _arrivalDate, value); }
        }

        public BasicAddOrEditResidentViewModel()
        {
            _selectedIndexGender = -1;
            _selectedIndexDorm = -1;
            _selectedIndexRoom = -1;
            _dateOfBirth = DateTime.Now;
            _dateOfIssue = DateTime.Now;
            _arrivalDate = DateTime.Now;
            _streets = DataManager.GetInstance().StreetsRepository.Read().ToList();
            _dorms = DataManager.GetInstance().DormsRepository.Read().ToList();
            _roomsData = DataManager.GetInstance().RoomsRepository.Read().ToList();
            _genders = ["М", "Ж"];
        }
        public Action OnApply { get; set; }
        public bool ConfirmApplyStatus { get; set; }
    }
}
