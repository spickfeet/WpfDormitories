using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfDormitories.DataBase;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.Model.FullName;
using WpfDormitories.Model.PersonDocument;
using WpfDormitories.Model.PersonDocument.Passport;
using WpfTest.ViewModel;

namespace WpfDormitories.ViewModel.ResidentsVM
{
    public class EditResidentViewModel : BasicAddOrEditResidentViewModel
    {
        private uint _id;
        private uint _originalRoomId;
        public override int SelectedIndexDorm
        {
            get { return _selectedIndexDorm; }
            set
            {
                Set(ref _selectedIndexDorm, value);
                SelectedIndexRoom = -1;
                _roomsData = DataManager.GetInstance().RoomsRepository.Read().ToList().
                    FindAll(item => (item.DormId == _dorms[SelectedIndexDorm].Id && (item.NumberFreePlaces > 0 || item.Id == _originalRoomId)));
                List<string> rooms = new();
                foreach (IRoomData room in _roomsData)
                    rooms.Add($"Комната №{room.NumberRoom}\nСвободных мест: {room.NumberFreePlaces} из {room.TotalNumberPlaces}");
                Rooms = rooms;
            }
        }
        public EditResidentViewModel(uint id, uint contractId, uint roomId, IPersonDocuments personDocuments,
            bool haveChildren, DateTime arrivalDate, float payment, string placeOfWork, string placeOfStudy)
        {
            _originalRoomId = roomId;
            _id = id;
            _contractId = contractId;
            SelectedIndexDorm = _dorms.FindIndex(item => item.Id == _roomsData.Find(item => item.Id == roomId).DormId);
            SelectedIndexRoom = _roomsData.FindIndex(item => item.Id == roomId);

            _surname = personDocuments.Passport.FullName.Surname;
            _name = personDocuments.Passport.FullName.Name;
            _patronymic = personDocuments.Passport.FullName.Patronymic;
            _selectedIndexGender = personDocuments.Passport.Gender == "М" ? 0 : 1;
            _haveChildren = haveChildren;

            _registrationNumber = personDocuments.RegistrationNumber;
            _seriesPassport = personDocuments.Passport.Series;
            _numberPassport = personDocuments.Passport.Number;
            _placeOfWork = placeOfWork;
            _payment = payment;

            _dateOfIssue = personDocuments.Passport.DateOfIssue;
            _whoGave = personDocuments.Passport.WhoGave;
            _dateOfBirth = personDocuments.Passport.DateOfBirth;
            _placeOfStudy = placeOfStudy;
            _arrivalDate = arrivalDate;
        }

        /// <summary>
        /// Изменить данные о жильце.
        /// </summary>
        public ICommand Apply
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (_selectedIndexDorm == -1 || _selectedIndexRoom == -1 ||
                    string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Name) ||
                    _selectedIndexGender == -1 || string.IsNullOrEmpty(RegistrationNumber) || string.IsNullOrEmpty(SeriesPassport) ||
                    string.IsNullOrEmpty(NumberPassport) || string.IsNullOrEmpty(WhoGave))
                    {
                        MessageBox.Show("Заполните все поля");
                        return;
                    }

                    OnApply?.Invoke();
                    if (ConfirmApplyStatus)
                    {
                        DataManager.GetInstance().ResidentsRepository.Update(new ResidentData(_id, _contractId, _roomsData[SelectedIndexRoom].Id,
                        new PersonDocuments(RegistrationNumber,
                        new Passport(new FullName(Surname, Name, Patronymic),
                        Genders[SelectedIndexGender], DateOfBirth, SeriesPassport, NumberPassport, DateOfIssue, WhoGave)),
                        HaveChildren, ArrivalDate, Payment, PlaceOfWork, PlaceOfStudy));
                        if(_originalRoomId != _roomsData[SelectedIndexRoom].Id)
                        {
                            IRoomData originalRoom = DataManager.GetInstance().RoomsRepository.Read().ToList().Find(item =>item.Id == _originalRoomId);
                            originalRoom.NumberFreePlaces += 1;
                            DataManager.GetInstance().RoomsRepository.Update(originalRoom);

                            DataManager.GetInstance().RoomsRepository.Update(new RoomData(_roomsData[SelectedIndexRoom].Id,
                                _roomsData[SelectedIndexRoom].DormId,
                                _roomsData[SelectedIndexRoom].NumberRoom, _roomsData[SelectedIndexRoom].RoomArea,
                                _roomsData[SelectedIndexRoom].TotalNumberPlaces, _roomsData[SelectedIndexRoom].Floor,
                                _roomsData[SelectedIndexRoom].NumberFreePlaces - 1));
                        }
                    }
                });
            }
        }
    }
}
