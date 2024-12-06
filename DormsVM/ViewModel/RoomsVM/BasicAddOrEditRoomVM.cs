using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.District;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase;

namespace WpfDormitories.ViewModel.RoomsVM
{
    public class BasicAddOrEditRoomVM : BasicVM, IApplicableVM
    {
        protected uint _dormId;
        protected string _numberRoom;
        protected uint _roomArea;
        protected uint _totalNumberPlace;
        protected uint _floor;
        protected uint _numberFreePlace;

        public string NumberRoom
        {
            get { return _numberRoom; }
            set { Set(ref _numberRoom, value); }
        }
        public uint RoomArea
        {
            get { return _roomArea; }
            set { Set(ref _roomArea, value); }
        }

        public uint TotalNumberPlace
        {
            get { return _totalNumberPlace; }
            set { Set(ref _totalNumberPlace, value); }
        }
        public uint Floor
        {
            get { return _floor; }
            set { Set(ref _floor, value); }
        }
        public uint NumberFreePlace
        {
            get { return _numberFreePlace; }
            set { Set(ref _numberFreePlace, value); }
        }

        public Action OnApply { get; set; }
        public bool ConfirmApplyStatus { get; set; }
    }
}
