using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Child.ParentsAndChildren;

namespace WpfDormitories.DataBase.Repositories
{
    public class ParentsAndChildrenRepository : IRepository<IParentsAndChildrenData>
    {
        public void Create(IParentsAndChildrenData entity)
        {
            string query = $"INSERT INTO parents_children (parent_id, child_id) VALUES ('{entity.ParentId}','{entity.ChildId}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IParentsAndChildrenData entity)
        {
            string query = $"DELETE FROM parents_children WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public ICollection<IParentsAndChildrenData> Read()
        {
            string query = "SELECT * FROM parents_children";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            ICollection<IParentsAndChildrenData> result = new Collection<IParentsAndChildrenData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new ParentsAndChildrenData(uint.Parse(row[0].ToString()), 
                    uint.Parse(row[1].ToString()), uint.Parse(row[2].ToString())));
            }
            return result;
        }

        public void Update(IParentsAndChildrenData entity)
        {
            string query = $"UPDATE `dormitory`.`parents_children` SET " +
                $"`parent_id` = '{entity.ParentId}', " +
                $"`child_id` = '{entity.ChildId}' " +
                $"WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
