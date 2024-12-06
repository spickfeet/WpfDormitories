using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Room;

namespace WpfDormitories.DataBase.Repositories
{
    public class RoomsRepository : IRepository<IRoomData>
    {
        public void Create(IRoomData entity)
        {
            string query = $"INSERT INTO rooms " +
                "(dorm_id, number_room, room_area, total_number_places, floor, number_free_places) " +
                $"VALUES ('{entity.DormId}','{entity.NumberRoom}','{entity.RoomArea}','{entity.TotalNumberPlaces}','{entity.Floor}','{entity.NumberFreePlaces}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IRoomData entity)
        {
            string query = $"DELETE FROM rooms WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public IList<IRoomData> Read()
        {
            string query = "SELECT * FROM rooms";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IRoomData> result = new List<IRoomData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(
                    new RoomData(uint.Parse(row[0].ToString()), 
                    uint.Parse(row[1].ToString()), row[2].ToString(),
                    uint.Parse(row[3].ToString()), uint.Parse(row[4].ToString()),
                    uint.Parse(row[5].ToString()), uint.Parse(row[6].ToString())));
            }
            return result;
        }

        public void Update(IRoomData entity)
        {
            string query = $"UPDATE `dormitory`.`rooms` SET " +
                $"`dorm_id` = '{entity.DormId}', " +
                $"`number_room` = '{entity.NumberRoom}', " +
                $"`room_area` = '{entity.RoomArea}', " +
                $"`total_number_places` = '{entity.TotalNumberPlaces}', " +
                $"`floor` = '{entity.Floor}', " +
                $"`number_free_places` = '{entity.NumberFreePlaces}' " +
                $"WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
