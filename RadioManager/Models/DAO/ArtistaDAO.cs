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
        public List<Artista> getArtistas()
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
                    String query = "SELECT * FROM radio_manager.cantante;";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();


                    while (dataReader.Read())
                    {
                        Artista artista = new Artista();

                        artista.IdArtista = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        artista.NombreArtistico = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        artista.Fotografia = (!dataReader.IsDBNull(2)) ? (byte[])dataReader.GetValue(2) : null;
                        artista.Descripcion = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        artista.Activo = (!dataReader.IsDBNull(4)) ? dataReader.GetBoolean(4): false ;

                        artistas.Add(artista);
                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción ArtistaDAO getArtistas():");
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

        public Artista getArtistaPorId(int idArtista)
        {
            Artista artista = new Artista();
            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = String.Format("SELECT * FROM cantante WHERE idCantante = '{0}';", idArtista);
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        artista.IdArtista = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        artista.NombreArtistico = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        artista.Fotografia = (!dataReader.IsDBNull(2)) ? (byte[])dataReader.GetValue(2) : null;
                        artista.Descripcion = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        artista.Activo = (!dataReader.IsDBNull(4)) ? dataReader.GetBoolean(4) : false;
                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción ArtistaDAO getArtistaPorId");
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
            return artista;
        }

        public Artista getArtistaPorNombre(String nombre)
        {
            Artista artista = new Artista();
            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = String.Format("SELECT * FROM cantante WHERE nombreArtistico = '{0}';", nombre);
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        artista.IdArtista = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        artista.NombreArtistico = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        artista.Fotografia = (!dataReader.IsDBNull(2)) ? (byte[])dataReader.GetValue(2) : null;
                        artista.Descripcion = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        artista.Activo = (!dataReader.IsDBNull(4)) ? dataReader.GetBoolean(4) : false;
                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción ArtistaDAO getArtistaPorId");
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
            return artista;
        }

        public bool registrarArtista(Artista artista)
        {
            bool registrado = false;
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO radio_manager.cantante VALUES(NULL, '" + artista.NombreArtistico + "', " + "@data" + ", '"+ artista.Descripcion + "', TRUE);", conn))
                    {
                        command.Parameters.AddWithValue("@data", artista.Fotografia);
                        command.ExecuteNonQuery();
                    }

                    /*MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = String.Format("INSERT INTO radio_manager.cantante VALUES (NULL,'{0}',[{1}],'{2}',TRUE);",
                        artista.NombreArtistico, artista.Fotografia, artista.Descripcion);
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                        dataReader.Close();
                    command.Dispose();*/

                    registrado = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción ArtisTaDAO registrarArtista");
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

            return registrado;
        }
    }
}
