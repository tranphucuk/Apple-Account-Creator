using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apple_Accounts_Creator.SSH
{
    public class DataController
    {
        private readonly string ConnectionString = $"Data Source={Application.StartupPath}\\ssh.db;Version=3;";
        private static DataController _instance;
        public static DataController Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new DataController();
                }
                return _instance;
            }
        }

        private SqliteConnection _conn;
        public DataController()
        {
            _conn = new SqliteConnection(ConnectionString);
        }

        public DataTable GetDataTable(string querry)
        {
            var table = new DataTable();
            _conn.Open();
            var cmd = new SqliteCommand(querry,_conn);
            var adapter = new SqliteDataAdapter(cmd);
            adapter.Fill(table);
            _conn.Close();
            return table;
        }

        public int ExecuteNonQuerry(string querry)
        {
            _conn.Open();
            var cmd = new SqliteCommand(querry, _conn);
            var rowAffected = cmd.ExecuteNonQuery();
            _conn.Close();

            return rowAffected;
        }
    }
}
