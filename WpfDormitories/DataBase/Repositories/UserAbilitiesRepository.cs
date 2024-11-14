using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.UserAbilities;

namespace WpfDormitories.DataBase.Repositories
{
    public class UserAbilitiesRepository : IRepository<IUserAbilitiesData>
    {
        public void Create(IUserAbilitiesData entity)
        {
            string query = $"INSERT INTO users_abilities " +
                "(user_id, menu_element_id, r, w, e,d ) " +
                $"VALUES ('{entity.UserId}',{entity.MenuElementId}, {entity.R}, {entity.W}, {entity.E}, {entity.D})";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IUserAbilitiesData entity)
        {
            string query = $"DELETE FROM users_abilities WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public IList<IUserAbilitiesData> Read()
        {
            string query = "SELECT * FROM users_abilities";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IUserAbilitiesData> result = new List<IUserAbilitiesData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(
                    new UserAbilitiesData(uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()), uint.Parse(row[2].ToString()),
                    int.Parse(row[3].ToString()) == 1 ? true : false, int.Parse(row[4].ToString()) == 1 ? true : false,
                    int.Parse(row[5].ToString()) == 1 ? true : false, int.Parse(row[6].ToString()) == 1 ? true : false));
            }
            return result;
        }

        public void Update(IUserAbilitiesData entity)
        {
            string query = $"UPDATE `dormitory`.`users_abilities` SET " +
                $"`user_id` = '{entity.UserId}', " +
                $"`menu_element_id` = '{entity.MenuElementId}', " +
                $"`r` = '{entity.R}', " +
                $"`w` = '{entity.W}', " +
                $"`e` = '{entity.E}', " +
                $"`d` = '{entity.D}' " +
                $"WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
