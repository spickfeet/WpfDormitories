using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.District;

namespace WpfDormitories.DataBase.Repositories
{
    public class DistrictsRepository : IRepository<IDistrictData>
    {
        public void Create(IDistrictData entity)
        {
            string query = $"INSERT INTO districts (name) VALUES ('{entity.Name}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IDistrictData entity)
        {
            string query = $"DELETE FROM districts WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public ICollection<IDistrictData> Read()
        {
            string query = "SELECT * FROM districts";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            ICollection<IDistrictData> result = new Collection<IDistrictData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new DistrictData(uint.Parse(row[0].ToString()), row[1].ToString()));
            }
            return result;
        }

        public void Update(IDistrictData entity)
        {
            string query = $"UPDATE `dormitory`.`districts` SET `name` = '{entity.Name}' WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
