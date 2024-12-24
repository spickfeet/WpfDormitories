using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.District;

namespace WpfDormitories.DataBase.Repositories
{
    /// <summary>
    /// Репозиторий для взаимодействия с таблицей районы.
    /// </summary>
    public class DistrictsRepository : IRepository<IDistrictData>
    {
        /// <summary>
        /// Создание новой записи в таблице районы.
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IDistrictData entity)
        {
            string query = $"INSERT INTO districts (name) VALUES ('{entity.Name}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        /// <summary>
        /// Удаление записи в таблице районы.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(IDistrictData entity)
        {
            string query = $"DELETE FROM districts WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        /// <summary>
        /// Чтение всех записей в таблице районы.
        /// </summary>
        /// <param name="entity"></param>
        public IList<IDistrictData> Read()
        {
            string query = "SELECT * FROM districts";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IDistrictData> result = new List<IDistrictData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new DistrictData(uint.Parse(row[0].ToString()), row[1].ToString()));
            }
            return result;
        }

        /// <summary>
        /// Обновление записи в таблице районы.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IDistrictData entity)
        {
            string query = $"UPDATE `dormitory`.`districts` SET `name` = '{entity.Name}' WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
