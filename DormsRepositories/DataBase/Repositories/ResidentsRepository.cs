using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Resident;
using WpfDormitories.Model.FullName;
using WpfDormitories.Model.PersonDocument.Passport;
using WpfDormitories.Model.PersonDocument;
using WpfDormitories.DataBase.Entity.Room;

namespace WpfDormitories.DataBase.Repositories
{
    public class ResidentsRepository : IRepository<IResidentData>
    {
        public void Create(IResidentData entity)
        {
            string query = $"INSERT INTO residents " +
                "(contract_id, room_id, registration_number, surname, name, patronymic, gender, " +
                "date_of_birth, series_passport, number_passport, date_of_issue, " +
                "who_gave, working, studying, have_children, arrival_date, payment, place_of_work, place_of_study) " +
                $"VALUES (" +
                $"'{entity.ContractId}'," +
                $"'{entity.RoomId}'," +
                $"'{entity.PersonDocuments.RegistrationNumber}'," +
                $"'{entity.PersonDocuments.Passport.FullName.Surname}'," +
                $"'{entity.PersonDocuments.Passport.FullName.Name}'," +
                $"'{(entity.PersonDocuments.Passport.FullName.Patronymic == null ? "" : entity.PersonDocuments.Passport.FullName.Patronymic)}'," +
                $"'{entity.PersonDocuments.Passport.Gender}'," +
                $"'{entity.PersonDocuments.Passport.DateOfBirth.ToString("yyyy-MM-dd")}'," +
                $"'{entity.PersonDocuments.Passport.Series}'," +
                $"'{entity.PersonDocuments.Passport.Number}'," +
                $"'{entity.PersonDocuments.Passport.DateOfIssue.ToString("yyyy-MM-dd")}'," +
                $"'{entity.PersonDocuments.Passport.WhoGave}'," +
                $"{!string.IsNullOrEmpty(entity.PlaceOfWork)}," +
                $"{!string.IsNullOrEmpty(entity.PlaceOfStudy)}," +
                $"{entity.HaveChildren}," +
                $"'{entity.ArrivalDate.ToString("yyyy-MM-dd")}'," +
                $"'{entity.Payment}'," +
                $"'{entity.PlaceOfWork}'," +
                $"'{entity.PlaceOfStudy}')";
            DormitorySQLConnection.GetInstance().Request(query);
            IRoomData roomsChange = DataManager.GetInstance().RoomsRepository.Read().ToList().Find(item => item.Id == entity.RoomId);
            DataManager.GetInstance().RoomsRepository.Update(new RoomData(roomsChange.Id,
                roomsChange.DormId, roomsChange.NumberRoom,
                roomsChange.RoomArea, roomsChange.TotalNumberPlaces,
                roomsChange.Floor, roomsChange.NumberFreePlaces - 1));
        }

        public void Delete(IResidentData entity)
        {
            string query = $"DELETE FROM residents WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
            IRoomData roomsChange = DataManager.GetInstance().RoomsRepository.Read().ToList().Find(item => item.Id == entity.RoomId);
            DataManager.GetInstance().RoomsRepository.Update(new RoomData(roomsChange.Id,
                roomsChange.DormId, roomsChange.NumberRoom,
                roomsChange.RoomArea, roomsChange.TotalNumberPlaces,
                roomsChange.Floor, roomsChange.NumberFreePlaces + 1));
        }

        public IList<IResidentData> Read()
        {
            string query = "SELECT * FROM residents";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IResidentData> result = new List<IResidentData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(
                    new ResidentData(
                        uint.Parse(row[0].ToString()),
                        uint.Parse(row[1].ToString()),
                        uint.Parse(row[2].ToString()),
                        new PersonDocuments(
                            row[3].ToString(),
                            new Passport(
                                new FullName(row[4].ToString(), row[5].ToString(), row[6]?.ToString()),
                                row[7].ToString(), DateTime.Parse(row[8].ToString()), (string)row[9],
                                (string)row[10], DateTime.Parse(row[11].ToString()), row[12].ToString())),
                        int.Parse(row[15].ToString()) == 1 ? true : false,
                        DateTime.Parse(row[16].ToString()),
                        (float)row[17],
                        row[18]?.ToString(),
                        row[19]?.ToString())
                    );
            }
            return result;
        }

        public void Update(IResidentData entity)
        {
            string query = $"UPDATE `dormitory`.`residents` SET " +
                $"`contract_id` = '{entity.ContractId}', " +
                $"`room_id` = '{entity.RoomId}', " +
                $"`registration_number` = '{entity.PersonDocuments.RegistrationNumber}', " +
                $"`surname` = '{entity.PersonDocuments.Passport.FullName.Surname}', " +
                $"`name` = '{entity.PersonDocuments.Passport.FullName.Name}', " +
                $"`patronymic` = '{(entity.PersonDocuments.Passport.FullName.Patronymic == null ? "" : entity.PersonDocuments.Passport.FullName.Patronymic)}', " +
                $"`gender` = '{entity.PersonDocuments.Passport.Gender}', " +
                $"`date_of_birth` = '{entity.PersonDocuments.Passport.DateOfBirth.ToString("yyyy-MM-dd")}', " +
                $"`series_passport` = '{entity.PersonDocuments.Passport.Series}', " +
                $"`number_passport` = '{entity.PersonDocuments.Passport.Number}', " +
                $"`date_of_issue` = '{entity.PersonDocuments.Passport.DateOfIssue.ToString("yyyy-MM-dd")}', " +
                $"`who_gave` = '{entity.PersonDocuments.Passport.WhoGave}', " +
                $"`working` = {!string.IsNullOrEmpty(entity.PlaceOfWork)}, " +
                $"`studying` = {!string.IsNullOrEmpty(entity.PlaceOfStudy)}, " +
                $"`have_children` = {entity.HaveChildren}," +
                $"`arrival_date` = '{entity.ArrivalDate.ToString("yyyy-MM-dd")}', " +
                $"`payment` = '{entity.Payment}', " +
                $"`place_of_work` = '{entity.PlaceOfWork}', " +
                $"`place_of_study` = '{entity.PlaceOfStudy}' " +
                $"WHERE (`id` = '{entity.Id}')";
                
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
