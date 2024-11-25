using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Room;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.RoomsVM
{
    public class AddRoomViewModel : BasicAddOrEditRoomVM
    {
        private uint _dormId;
        public AddRoomViewModel(uint dormId)
        {
            _dormId = dormId;
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
                        DataManager.GetInstance().RoomsRepository.
                        Create(new RoomData(_dormId, NumberRoom, RoomArea, TotalNumberPlace, Floor, NumberFreePlace));
                    }
                });
            }
        }
    }
}
