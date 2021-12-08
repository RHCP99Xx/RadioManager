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
    class CategoriaDAO
    {
        public List<Categoria> getCategorias()
        {
            List<Categoria> Categorias = new List<Categoria>();
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT * FROM radio_manager.categoria;";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();


                    while (dataReader.Read())
                    {
                        Categoria Categoria = new Categoria();

                        Categoria.IdCategoria = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        Categoria.NombreCategoria = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";


                        Categorias.Add(Categoria);

                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción CategoriaDAO getCategorias():");
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
            return Categorias;
        }

    }
}
