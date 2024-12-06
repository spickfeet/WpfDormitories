using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDormitories.DataBase.Entity.Child.ParentsAndChildren;
using WpfDormitories.DataBase.Entity.MenuElement;

namespace WpfDormitories.DataBase.Repositories
{
    public class MenuElementsRepository : IRepository<IMenuElementData>
    {
        public void Create(IMenuElementData entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IMenuElementData entity)
        {
            throw new NotImplementedException();
        }

        public IList<IMenuElementData> Read()
        {
            string query = "SELECT * FROM menu_elements";

            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IMenuElementData> result = new List<IMenuElementData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new MenuElementData(uint.Parse(row[0].ToString()), uint.Parse(row[1].ToString()), row[2].ToString(),
                    row[3]?.ToString(), row[4]?.ToString(), uint.Parse(row[5].ToString())));
            }
            return result;
        }

        public void Update(IMenuElementData entity)
        {
            throw new NotImplementedException();
        }
    }
}
