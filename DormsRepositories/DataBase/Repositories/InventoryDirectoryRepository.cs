using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Inventory.InventoryDirectory;

namespace WpfDormitories.DataBase.Repositories
{
    /// <summary>
    /// Репозиторий для взаимодействия с таблицей справочник инвентаря.
    /// </summary>
    public class InventoryDirectoryRepository : IRepository<IInventoryDirectoryData>
    {
        /// <summary>
        /// Создание новой записи в таблице справочник инвентаря.
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IInventoryDirectoryData entity)
        {
            string query = $"INSERT INTO inventory_directory (name) VALUES ('{entity.Name}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        /// <summary>
        /// Удаление записи в таблице справочник инвентаря.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(IInventoryDirectoryData entity)
        {
            string query = $"DELETE FROM inventory_directory WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        /// <summary>
        /// Чтение всех записей в таблице справочник инвентаря.
        /// </summary>
        /// <param name="entity"></param>
        public IList<IInventoryDirectoryData> Read()
        {
            string query = "SELECT * FROM inventory_directory";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IInventoryDirectoryData> result = new List<IInventoryDirectoryData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new InventoryDirectoryData(uint.Parse(row[0].ToString()), row[1].ToString()));
            }
            return result;
        }

        /// <summary>
        /// Обновление записи в таблице справочник инвентаря.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IInventoryDirectoryData entity)
        {
            string query = $"UPDATE `dormitory`.`inventory_directory` SET `name` = '{entity.Name}' WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
