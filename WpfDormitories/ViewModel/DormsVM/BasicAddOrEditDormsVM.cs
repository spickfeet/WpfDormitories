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
    public class BasicAddOrEditDormsVM : BasicVM, IApplicableVM
    {
        protected IList<IStreetData> _streets;
        protected IList<IDistrictData> _districts;
        protected int _selectedStreetIndex;
        protected int _selectedDistrictIndex;

        protected string _dormNumber;
        protected string _houseNumber;

        protected uint _numberRooms;
        protected uint _numberPlace;

        public IList<string> Streets
        {
            get
            {
                IList<string> res = new List<string>();
                foreach (IStreetData street in _streets)
                {
                    res.Add(street.Name);
                }
                return res;
            }
        }

        public IList<string> Districts
        {
            get
            {
                IList<string> res = new List<string>();
                foreach (IDistrictData district in _districts)
                {
                    res.Add(district.Name);
                }
                return res;
            }
        }

        public int SelectedStreetIndex 
        { 
            get { return _selectedStreetIndex; }
            set { Set<int>(ref _selectedStreetIndex, value); }
        }

        public int SelectedDistrictIndex
        {
            get { return _selectedDistrictIndex; }
            set { Set<int>(ref _selectedDistrictIndex, value); }
        }

        public string DormNumber
        {
            get { return _dormNumber; }
            set { Set<string>(ref _dormNumber, value); }
        }
        public string HouseNumber
        {
            get { return _houseNumber; }
            set { Set<string>(ref _houseNumber, value); }
        }

        public uint NumberRooms
        {
            get { return _numberRooms; }
            set { Set<uint>(ref _numberRooms, value); }
        }
        public uint NumberPlace
        {
            get { return _numberPlace; }
            set { Set<uint>(ref _numberPlace, value); }
        }

        public Action OnApply { get; set; }
        public bool ConfirmApplyStatus { get; set; }

        public BasicAddOrEditDormsVM() 
        {
            _streets = DataManager.GetInstance().StreetsRepository.Read();
            _districts = DataManager.GetInstance().DistrictsRepository.Read();
            _selectedDistrictIndex = -1;
            _selectedStreetIndex = -1;
        }
    }
}
