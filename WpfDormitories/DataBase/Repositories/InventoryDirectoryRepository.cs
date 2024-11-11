using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Inventory.InventoryDirectory;

namespace WpfDormitories.DataBase.Repositories
{
    public class InventoryDirectoryRepository : IRepository<IInventoryDirectoryData>
    {
        public void Create(IInventoryDirectoryData entity)
        {
            string query = $"INSERT INTO inventory_directory (name) VALUES ('{entity.Name}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IInventoryDirectoryData entity)
        {
            string query = $"DELETE FROM inventory_directory WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public ICollection<IInventoryDirectoryData> Read()
        {
            string query = "SELECT * FROM inventory_directory";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            ICollection<IInventoryDirectoryData> result = new Collection<IInventoryDirectoryData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new InventoryDirectoryData(uint.Parse(row[0].ToString()), row[1].ToString()));
            }
            return result;
        }

        public void Update(IInventoryDirectoryData entity)
        {
            string query = $"UPDATE `dormitory`.`inventory_directory` SET `name` = '{entity.Name}' WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
