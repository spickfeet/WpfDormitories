using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Inventory;

namespace WpfDormitories.DataBase.Repositories
{
    public class InventoryRepository : IRepository<IInventoryData>
    {
        public void Create(IInventoryData entity)
        {
            string query = $"INSERT INTO inventory (room_id, name_id) VALUES ('{entity.RoomId}','{entity.NameId}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IInventoryData entity)
        {
            string query = $"DELETE FROM inventory WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public IList<IInventoryData> Read()
        {
            string query = "SELECT * FROM inventory";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IInventoryData> result = new List<IInventoryData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new InventoryData(uint.Parse(row[0].ToString()), uint.Parse(row[1].ToString()), uint.Parse(row[2].ToString())));
            }
            return result;
        }

        public void Update(IInventoryData entity)
        {
            string query = $"UPDATE `dormitory`.`inventory` SET " +
                $"`room_id` = '{entity.RoomId}', " +
                $"`name_id` = '{entity.NameId}' " +
                $"WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
