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
    class ArtistaDAO
    {
        public List<Artista> getArtistasSinImagen()
        {
            List<Artista> artistas = new List<Artista>();
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT idCantante, nombreArtistico FROM radio_manager.cantante;;";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();


                    while (dataReader.Read())
                    {
                        Artista artista = new Artista();

                        artista.IdArtista = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        artista.NombreArtistico = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";


                        artistas.Add(artista);

                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción ArtistaDAO getArtistasSinImagen():");
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
            return artistas;
        }
    }
}
