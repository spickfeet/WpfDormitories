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
    public class AddResidentViewModel : BasicAddOrEditResidentViewModel
    {
        public AddResidentViewModel(uint contractId)
        {
            _contractId = contractId;
        }

        /// <summary>
        /// Добавить данные о жильце.
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
                        DataManager.GetInstance().ResidentsRepository.
                        Create(new ResidentData(_contractId, _roomsData[SelectedIndexRoom].Id,
                        new PersonDocuments(RegistrationNumber, 
                        new Passport(new FullName(Surname, Name, Patronymic), 
                        Genders[SelectedIndexGender], DateOfBirth, SeriesPassport, NumberPassport, DateOfIssue, WhoGave)),
                        HaveChildren, ArrivalDate, Payment,PlaceOfWork,PlaceOfStudy));
                    }
                });
            }
        }
    }
}
