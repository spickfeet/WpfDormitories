using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Eviction;
using WpfDormitories.Model.FullName;
using WpfDormitories.Model.PersonDocument;
using WpfDormitories.Model.PersonDocument.Passport;

namespace WpfDormitories.DataBase.Repositories
{
    public class EvictionsRepository : IRepository<IEvictionData>
    {
        public void Create(IEvictionData entity)
        {
            string query = $"INSERT INTO evictions " +
                "(registration_number, surname, name, patronymic, gender, " +
                "date_of_birth, series_passport, number_passport, date_of_issue, " +
                "who_gave, reason, date) " +
                $"VALUES ('{entity.PersonDocuments.RegistrationNumber}'," +
                $"'{entity.PersonDocuments.Passport.FullName.Surname}'," +
                $"'{entity.PersonDocuments.Passport.FullName.Name}'," +
                $"'{(entity.PersonDocuments.Passport.FullName.Patronymic == null ? "" : entity.PersonDocuments.Passport.FullName.Patronymic)}'," +
                $"'{entity.PersonDocuments.Passport.Gender}'," +
                $"'{entity.PersonDocuments.Passport.DateOfBirth.ToString("yyyy-MM-dd")}'," +
                $"'{entity.PersonDocuments.Passport.Series}'," +
                $"'{entity.PersonDocuments.Passport.Number}'," +
                $"'{entity.PersonDocuments.Passport.DateOfIssue.ToString("yyyy-MM-dd")}'," +
                $"'{entity.PersonDocuments.Passport.WhoGave}'," +
                $"'{entity.Reason}'," +
                $"'{entity.Date.ToString("yyyy-MM-dd")}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IEvictionData entity)
        {
            string query = $"DELETE FROM evictions WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public IList<IEvictionData> Read()
        {
            string query = "SELECT * FROM evictions";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IEvictionData> result = new List<IEvictionData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(
                    new EvictionData(
                        uint.Parse(row[0].ToString()),
                        new PersonDocuments(
                            row[1].ToString(),
                            new Passport(
                                new FullName(row[2].ToString(), row[3].ToString(), row[4]?.ToString()),
                                row[5].ToString(), DateOnly.Parse(row[6].ToString()), uint.Parse(row[7].ToString()), 
                                uint.Parse(row[8].ToString()),DateOnly.Parse(row[9].ToString()), row[10].ToString())),
                        row[11].ToString(),
                        DateOnly.Parse(row[12].ToString()))
                    );
            }
            return result;
        }

        public void Update(IEvictionData entity)
        {
            string query = $"UPDATE `dormitory`.`evictions` SET " +
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
                $"`reason` = '{entity.Reason}', " +
                $"`date` = '{entity.Date.ToString("yyyy-MM-dd")}' " +
                $"WHERE (`id` = '{entity.Id}')";

            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
