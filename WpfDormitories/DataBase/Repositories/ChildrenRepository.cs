using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Child;
using WpfDormitories.DataBase.Entity.Room;
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
                $"'{entity.FullName.Name}','{entity.FullName.Patronymic}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IChildData entity)
        {
            string query = $"DELETE FROM children WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public ICollection<IChildData> Read()
        {
            string query = "SELECT * FROM children";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            ICollection<IChildData> result = new Collection<IChildData>();
            foreach (DataRow row in dt.Rows)
            {

                string[] dateString = row[2].ToString().Split("-");
                int[] dateInt = new int[3] 
                { 
                    int.Parse(dateString[0]), int.Parse(dateString[1]), int.Parse(dateString[2]) 
                };
                DateOnly date = new(dateInt[0], dateInt[1], dateInt[2]);

                result.Add(
                    new ChildData(uint.Parse(row[0].ToString()),
                    row[1].ToString(), date, new FullName(row[2].ToString(), row[3].ToString(), row[4].ToString())
                    ));
            }
            return result;
        }

        public void Update(IChildData entity)
        {
            string query = $"UPDATE `dormitory`.`children` SET " +
                $"`gender` = '{entity.Gender}', " +
                $"`date_of_birth` = '{entity.DateOfBirth.ToString("yyyy-MM-dd")}' " +
                $"`surname` = '{entity.FullName.Surname}' " +
                $"`name` = '{entity.FullName.Name}' " +
                $"`patronymic` = '{entity.FullName.Patronymic}' " +
                $"WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
