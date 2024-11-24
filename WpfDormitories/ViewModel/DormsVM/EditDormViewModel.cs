using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Dorm;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.DormsVM
{
    public class EditDormViewModel : BasicAddOrEditDormsVM
    {
        private uint _id;
        public EditDormViewModel(uint id, uint streetId, uint districtsId, 
            string dormNumber, string houseNumber, uint numberRooms, uint numberPlace)
        {
            _id = id;
            _selectedStreetIndex = _streets.ToList().FindIndex(item => item.Id == streetId);
            _selectedDistrictIndex = _districts.ToList().FindIndex(item => item.Id == districtsId);
            _dormNumber = dormNumber;
            _houseNumber = houseNumber;
            _numberRooms = numberRooms;
            _numberPlace = numberPlace;
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
                        DataManager.GetInstance().DormsRepository.Update(new DormData(_id, _streets[SelectedStreetIndex].Id,
                        _districts[SelectedDistrictIndex].Id, DormNumber, HouseNumber, NumberRooms, NumberPlace));
                    }
                });
            }
        }
    }
}
