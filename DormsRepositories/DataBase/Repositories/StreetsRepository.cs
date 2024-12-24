using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Street;

namespace WpfDormitories.DataBase.Repositories
{
    /// <summary>
    /// Репозиторий для взаимодействия с таблицей улицы.
    /// </summary>
    public class StreetsRepository : IRepository<IStreetData>
    {
        /// <summary>
        /// Создание новой записи в таблице улицы.
        /// </summary>
        /// <param name="entity"></param>
        public void Create(IStreetData entity)
        {
            string query = $"INSERT INTO streets (name) VALUES ('{entity.Name}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        /// <summary>
        /// Удаление записи в таблице улицы.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(IStreetData entity)
        {
            string query = $"DELETE FROM streets WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        /// <summary>
        /// Чтение всех записей в таблице улицы.
        /// </summary>
        /// <param name="entity"></param>
        public IList<IStreetData> Read()
        {
            string query = "SELECT * FROM streets";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IStreetData> result = new List<IStreetData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new StreetData(uint.Parse(row[0].ToString()), row[1].ToString()));
            }
            return result;
        }

        /// <summary>
        /// Обновление записи в таблице улицы.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(IStreetData entity)
        {
            string query = $"UPDATE `dormitory`.`streets` SET `name` = '{entity.Name}' WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
