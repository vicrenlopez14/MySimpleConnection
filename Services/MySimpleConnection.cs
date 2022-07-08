using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace MySimpleConnection.Services
{
    public class MySimpleConnection
    {
        public string username { get; set; }
        public string host { get; set; }
        public string database { get; set; }

        public string connectionString { get; set; }

        public string port { get; set; }
        public string password { get; set; }

        public static MySqlConnection con { get; set; }

        public MySimpleConnection(string host, string username, string database, string port)
        {
            this.host = host;
            this.username = username;
            this.database = database;
            this.port = port;

            connectionString = $"server={this.host};uid={this.username};database={this.database};port={port};";

            con = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetMySqlConnectionObj()
        {
            return con;
        }

        public static DataTable ExecuteQueryInDatabase(string sentence)
        {
            DataTable dt = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter(sentence, con);

            adapter.Fill(dt);

            return dt;
        }

        public static bool ExecuteNonQueryInDatabase(string sentence)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(sentence, con);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
