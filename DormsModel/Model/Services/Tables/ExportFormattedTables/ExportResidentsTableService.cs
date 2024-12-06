using WpfDormitories.DataBase.Entity.Dorm;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.DataBase.Entity.Room;
using WpfDormitories.DataBase;
using System.Data;
using WpfDormitories.DataBase.Entity.Contract;

namespace WpfDormitories.Model.Services.Tables.ExportFormattedTables
{
    public class ExportResidentsTableService : ResidentsTableService
    {
        private List<IContractData> _contracts;
        public ExportResidentsTableService()
        {
            _contracts = DataManager.GetInstance().ContractsRepository.Read().ToList();
        }
        public virtual DataTable Read()
        {
            List<IResidentData> residents = DataManager.GetInstance().ResidentsRepository.Read().ToList();

            DataTable res = CreateDataTable();
            foreach (IResidentData resident in residents)
            {
                IRoomData room = _rooms.Find(item => item.Id == resident.RoomId);
                IDormData dorm = _dorms.Find(item => item.Id == room.DormId);
                IContractData contract = _contracts.Find(item => item.Id == resident.ContractId);
                res.Rows.Add(contract.DocumentNumber, contract.Name, contract.WhoGave, contract.StartAction.ToString("yyyy-MM-dd")
                    , ($"Общежитие №{dorm.DormNumber}, {_streets.Find(item => item.Id == dorm.StreetId).Name}"),
                    room.NumberRoom,
                    resident.PersonDocuments.RegistrationNumber,
                    resident.PersonDocuments.Passport.FullName.Surname,
                    resident.PersonDocuments.Passport.FullName.Name,
                    resident.PersonDocuments.Passport.FullName.Patronymic,
                    resident.PersonDocuments.Passport.Gender,
                    resident.PersonDocuments.Passport.DateOfBirth.ToString("yyyy-MM-dd"),
                    resident.PersonDocuments.Passport.Series,
                    resident.PersonDocuments.Passport.Number,
                    resident.PersonDocuments.Passport.DateOfIssue.ToString("yyyy-MM-dd"),
                    resident.PersonDocuments.Passport.WhoGave,
                    string.IsNullOrEmpty(resident.PlaceOfWork) ? "Нет" : "Да",
                    string.IsNullOrEmpty(resident.PlaceOfStudy) ? "Нет" : "Да",
                    resident.HaveChildren ? "Да" : "Нет",
                    resident.ArrivalDate.ToString("yyyy-MM-dd"),
                    resident.Payment,
                    resident.PlaceOfWork,
                    resident.PlaceOfStudy);
            }
            return res;
        }
        private DataTable CreateDataTable()
        {
            DataTable res = new();
            res.Columns.Add("Номер документа заселения");
            res.Columns.Add("Название документа заселения");
            res.Columns.Add("Кем выдан документ заселения");
            res.Columns.Add("Начало действия документа заселения");

            res.Columns.Add("Общежитие");
            res.Columns.Add("Номер комнаты");
            res.Columns.Add("Регистрационный номер");
            res.Columns.Add("Фамилия");
            res.Columns.Add("Имя");
            res.Columns.Add("Отчество");
            res.Columns.Add("Пол");
            res.Columns.Add("Дата рождения");
            res.Columns.Add("Серия паспорта");
            res.Columns.Add("Номер паспорта");
            res.Columns.Add("Дата выдачи паспорта");
            res.Columns.Add("Кем выдан паспорт");
            res.Columns.Add("Работает");
            res.Columns.Add("Учится");
            res.Columns.Add("Есть дети");
            res.Columns.Add("Дата заселения");
            res.Columns.Add("Месячная плата");
            res.Columns.Add("Место работы");
            res.Columns.Add("Место учебы");
            return res;
        }
    }
}
