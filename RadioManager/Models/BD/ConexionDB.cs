using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

namespace RadioManager.Models.BD
{
    public class ConexionDB
    {
        private static string server = "maisonbleue2020.ddns.net";
        private static string database = "radio_manager";
        private static string user = "edgarhv";
        private static string password = "30dpr4319n#M";

        private static string connectionString = connectionString = "SERVER=" + server + "; PORT = 3306 ;" + 
            "DATABASE=" + database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";

        public static MySqlConnection getConnection()
        {
            MySqlConnection connection = null;

            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
            }
            catch (Exception exception)
            {
                Console.WriteLine("\nExcepcion Conexión BD");
                Console.WriteLine(exception.Message);
                throw;
            }
            Console.WriteLine("\nConexión exitosa\n");
            return connection;
        }
    }
}
