using MySqlConnector;
using System.Data;

namespace WpfDormitories.DataBase
{
    public class DormitorySQLConnection
    {
        private readonly string _connectionString = "SERVER=localhost;DATABASE=dormitory;UID=root;PASSWORD=root;";
        private readonly MySqlConnection _connection;

        private static DormitorySQLConnection _instance;

        private DormitorySQLConnection()
        {
            _connection = new(_connectionString);
        }

        public static DormitorySQLConnection GetInstance()
        {
            if (_instance == null)
                _instance = new DormitorySQLConnection();
            return _instance;
        }

        public void Request(string query)
        {
            _connection.Open();

            MySqlCommand command = new(query, _connection);

            try
            {
                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                _connection.Close();
                throw ex;
            }
        }

        public DataTable GetData(string query)
        {
            _connection.Open();

            MySqlCommand command = new(query, _connection);

            MySqlDataReader reader;
            try
            {
                reader = command.ExecuteReader();
            }

            catch (Exception ex)
            {
                _connection.Close();
                throw ex;
            }

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            _connection.Close();
            return dataTable;
        }

    }
}
