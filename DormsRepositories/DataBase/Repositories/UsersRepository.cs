using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.User;

namespace WpfDormitories.DataBase.Repositories
{
    /// <summary>
    /// Репозиторий для взаимодействия с таблицей пользователи.
    /// </summary>
    public class UsersRepository : IRepository<IUserData>
    {
        /// <summary>
        /// Репозиторий для взаимодействия с таблицей пользователи.
        /// </summary>
        public void Create(IUserData entity)
        {
            string query = $"INSERT INTO users (surname, name, patronymic, login, password) VALUES ('{entity.Surname}','{entity.Name}','{entity.Patronymic}','{entity.User.Login}','{entity.User.Password}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        /// <summary>
        /// Удаление записи в таблице пользователи.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(IUserData entity)
        {
            string query = $"DELETE FROM users WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        /// <summary>
        /// Чтение всех записей в таблице пользователи.
        /// </summary>
        /// <param name="entity"></param>
        public IList<IUserData> Read()
        {
            string query = "SELECT * FROM users";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IUserData> result = new List<IUserData>();
            foreach (DataRow row in dt.Rows) 
            {
                result.Add(new UserData(uint.Parse(row[0].ToString()), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[1].ToString(), row[2].ToString()));
            }
            return result;
        }

        /// <summary>
        /// Обновление записи в таблице пользователи.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IUserData entity)
        {
            string query = $"UPDATE `dormitory`.`users` " +
                $"SET `login` = '{entity.User.Login}', " +
                $"`password` = '{entity.User.Password}' " +
                $"`surname` = '{entity.Surname}' " +
                $"`name` = '{entity.Name}' " +
                $"`patronymic` = '{entity.Patronymic}' " +
                $"WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
