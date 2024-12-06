using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.Model.FullName;

namespace WpfDormitories.DataBase.Repositories
{
    public class ChildrenRepository : IRepository<IChildData>
    {
        public void Create(IChildData entity)
        {
            string query = $"INSERT INTO children " +
                "(gender, date_of_birth, surname, name, patronymic) " +
                $"VALUES ('{entity.Gender}','{entity.DateOfBirth.ToString("yyyy-MM-dd")}','{entity.FullName.Surname}'," +
                $"'{entity.FullName.Name}','{(entity.FullName.Patronymic == null ? "" : entity.FullName.Patronymic)}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IChildData entity)
        {
            string query = $"DELETE FROM children WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public IList<IChildData> Read()
        {
            string query = "SELECT * FROM children";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IChildData> result = new List<IChildData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(
                    new ChildData(uint.Parse(row[0].ToString()),
                    row[1].ToString(), DateTime.Parse(row[2].ToString()), new FullName(row[3].ToString(), row[4].ToString(), row[5]?.ToString())
                    ));
            }
            return result;
        }

        public void Update(IChildData entity)
        {
            string query = $"UPDATE `dormitory`.`children` SET " +
                $"`gender` = '{entity.Gender}', " +
                $"`date_of_birth` = '{entity.DateOfBirth.ToString("yyyy-MM-dd")}', " +
                $"`surname` = '{entity.FullName.Surname}', " +
                $"`name` = '{entity.FullName.Name}', " +
                $"`patronymic` = '{(entity.FullName.Patronymic == null ? "" : entity.FullName.Patronymic)}' " +
                $"WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
