using MySql.Data.MySqlClient;
using RadioManager.Models.BD;
using RadioManager.Models.POCO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.DAO
{
    class GeneroDAO
    {
        public List<Genero> getGeneros()
        {
            List<Genero> generos = new List<Genero>();
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT * FROM radio_manager.genero;";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();


                    while (dataReader.Read())
                    {
                        Genero genero = new Genero();

                        genero.IdGenero = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        genero.NombreGenero = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";


                        generos.Add(genero);

                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción GeneroDAO getGeneros():");
                Console.WriteLine(e.Message);
                Console.WriteLine("----------------------------------------------------------------\n");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return generos;
        }

    }
}
