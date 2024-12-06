using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Dorm;

namespace WpfDormitories.DataBase.Repositories
{
    public class DormsRepository : IRepository<IDormData>
    {
        public void Create(IDormData entity)
        {
            string query = $"INSERT INTO dorms " +
                "(street_id, district_id, dorm_number, house_number, number_rooms, number_places) " +
                $"VALUES ('{entity.StreetId}','{entity.DistrictId}','{entity.DormNumber}'," +
                $"'{entity.HouseNumber}','{entity.NumberRooms}','{entity.NumberPlace}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IDormData entity)
        {
            string query = $"DELETE FROM dorms WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public IList<IDormData> Read()
        {
            string query = "SELECT * FROM dorms";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IDormData> result = new List<IDormData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(
                    new DormData(uint.Parse(row[0].ToString()),
                    uint.Parse(row[1].ToString()), uint.Parse(row[2].ToString()),
                    row[3].ToString(), row[4].ToString(),
                    uint.Parse(row[5].ToString()), uint.Parse(row[6].ToString())));
            }
            return result;
        }

        public void Update(IDormData entity)
        {
            string query = $"UPDATE `dormitory`.`dorms` SET " +
                $"`street_id` = '{entity.StreetId}', " +
                $"`district_id` = '{entity.DistrictId}', " +
                $"`dorm_number` = '{entity.DormNumber}', " +
                $"`house_number` = '{entity.HouseNumber}', " +
                $"`number_rooms` = '{entity.NumberRooms}', " +
                $"`number_places` = '{entity.NumberPlace}' " +
                $"WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
