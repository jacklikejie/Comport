using System.Data;
using System.Data.SQLite;

namespace Comport.ORM
{
    internal class SQLiteClient
    {
        private SQLiteConnection m_dbConnection;

        public string DBPath { get; private set; }

        public bool Connected => m_dbConnection != null && m_dbConnection.State != ConnectionState.Closed;

        public SQLiteClient(string dbPath)
        {
            DBPath = dbPath;
        }

        public void Connect()
        {
            string connectionString = $"Data Source={DBPath};Version=3;";
            m_dbConnection = new SQLiteConnection(connectionString);
            m_dbConnection.Open();
        }

        public int Excute(string sql)
        {
            if (!Connected)
            {
                Connect();
            }

            SQLiteCommand sQLiteCommand = new SQLiteCommand(sql, m_dbConnection);
            return sQLiteCommand.ExecuteNonQuery();
        }

        public DataTable Query(string sql)
        {
            if (!Connected)
            {
                Connect();
            }

            DataTable dataTable = new DataTable();
            SQLiteCommand cmd = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd);
            sQLiteDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public void Close()
        {
            if (m_dbConnection != null)
            {
                m_dbConnection.Close();
            }
        }
    }
}