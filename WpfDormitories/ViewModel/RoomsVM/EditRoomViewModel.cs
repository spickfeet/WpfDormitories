using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Room;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.RoomsVM
{
    public class EditRoomViewModel : BasicAddOrEditRoomVM
    {
        private uint _id;
        public EditRoomViewModel(uint id, uint dormId, string numberRoom, uint roomArea, uint totalNumberPlace, uint floor, uint numberFreePlace)
        {
            _id = id;
            _dormId = dormId;
            _numberRoom = numberRoom;
            _roomArea = roomArea;
            _totalNumberPlace = totalNumberPlace;
            _floor = floor;
            _numberFreePlace = numberFreePlace;
        }
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (string.IsNullOrEmpty(NumberRoom) || RoomArea == 0 || TotalNumberPlace == 0)
                    {
                        MessageBox.Show("Заполните все поля");
                        return;
                    }
                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataManager.GetInstance().RoomsRepository.Update(new RoomData(_id, _dormId,NumberRoom,RoomArea,TotalNumberPlace,Floor,NumberFreePlace));
                    }
                });
            }
        }
    }
}
