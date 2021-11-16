using System;
using System.Data.SqlClient;

namespace RadioManager.Models.BD
{
    public class ConexionDB
    {
        private static string SERVER = "maisonbleue2020.ddns.net";
        private static string PORT = "3306";
        private static string DATABASE = "radio_manager";
        private static string USER = "edgarhv";
        private static string PASSWORD = "30dpr4319n#M";

        public static SqlConnection getConnection()
        {
            SqlConnection connection = null;

            try
            {
                String urlconn = String.Format(
                    "Data Source={0}, {1};" +
                    "Network Library=DBMSSOCN;" +
                    "User ID={3};" +
                    "Password={4};",
                    SERVER, PORT, DATABASE, USER, PASSWORD);
                connection = new SqlConnection(urlconn);
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
