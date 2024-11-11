using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.User;

namespace WpfDormitories.DataBase.Repositories
{
    public class UsersRepository : IRepository<IUserData>
    {
        public void Create(IUserData entity)
        {
            string query = $"INSERT INTO users (login, password) VALUES ('{entity.User.Login}','{entity.User.Password}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IUserData entity)
        {
            string query = $"DELETE FROM users WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public ICollection<IUserData> Read()
        {
            string query = "SELECT * FROM users";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            ICollection<IUserData> result = new Collection<IUserData>();
            foreach (DataRow row in dt.Rows) 
            {
                result.Add(new UserData(uint.Parse(row[0].ToString()), row[1].ToString(), row[2].ToString()));
            }
            return result;
        }

        public void Update(IUserData entity)
        {
            string query = $"UPDATE `dormitory`.`users` SET `login` = '{entity.User.Login}', `password` = '{entity.User.Password}' WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
