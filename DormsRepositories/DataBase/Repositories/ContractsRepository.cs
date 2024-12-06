using System.Collections.ObjectModel;
using System.Data;
using WpfDormitories.DataBase.Entity.Contract;

namespace WpfDormitories.DataBase.Repositories
{
    public class ContractsRepository : IRepository<IContractData>
    {
        public void Create(IContractData entity)
        {
            string query = $"INSERT INTO contracts " +
                "(document_number, name, who_gave, start_action, comment) " +
                $"VALUES ('{entity.DocumentNumber}','{entity.Name}','{entity.WhoGave}','{entity.StartAction.ToString("yyyy-MM-dd")}','{entity.Comment}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public void Delete(IContractData entity)
        {
            string query = $"DELETE FROM contracts WHERE id={entity.Id}";
            DormitorySQLConnection.GetInstance().Request(query);
        }

        public IList<IContractData> Read()
        {
            string query = "SELECT * FROM contracts";
            DataTable dt = DormitorySQLConnection.GetInstance().GetData(query);
            IList<IContractData> result = new List<IContractData>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(
                    new ContractData(uint.Parse(row[0].ToString()),
                    row[1].ToString(), row[2].ToString(),
                    row[3].ToString(), DateTime.Parse(row[4].ToString()),
                    row[5].ToString()));
            }
            return result;
        }

        public void Update(IContractData entity)
        {
            string query = $"UPDATE `dormitory`.`contracts` SET " +
                $"document_number = '{entity.DocumentNumber}', " +
                $"`name` = '{entity.Name}', " +
                $"`who_gave` = '{entity.WhoGave}', " +
                $"`start_action` = '{entity.StartAction.ToString("yyyy-MM-dd")}', " +
                $"`comment` = '{entity.Comment}' " +
                $"WHERE (`id` = '{entity.Id}')";
            DormitorySQLConnection.GetInstance().Request(query);
        }
    }
}
