using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Street;
using WpfDormitories.DataBase.Entity.User;

namespace WpfDormitories.DataBase.Repositories
{
    public class StreetsRepository : IRepository<IStreetData>
    {
        public void Create(IStreetData entity)
        {
            string query = $"INSERT INTO streets (name) VALUES ('{entity.Name}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IStreetData entity)
        {
            string query = $"DELETE FROM streets WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public ICollection<IStreetData> Read()
        {
            string query = "SELECT * FROM streets";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            ICollection<IStreetData> result = new Collection<IStreetData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new StreetData(uint.Parse(row[0].ToString()), row[1].ToString()));
            }
            return result;
        }

        public void Update(IStreetData entity)
        {
            string query = $"UPDATE `dormitory`.`streets` SET `name` = '{entity.Name}' WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
