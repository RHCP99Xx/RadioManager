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
    class CancionDAO
    {
        public List<Cancion> getCancionesReporte(Artista artista, Categoria categoria, Genero genero)
        {
            List<Cancion> canciones = new List<Cancion>();
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT idCancion, claves, titulo, dias, activo, alAire " +
                                    "FROM radio_manager.cancion WHERE " +
                                    "idCantante = " + artista.IdArtista +
                                    " AND idCategoria = " + categoria.IdCategoria + 
                                    " AND isGenero = " + genero.IdGenero + ";";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();


                    while (dataReader.Read())
                    {
                        Cancion cancion = new Cancion();

                        cancion.IdCancion = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        cancion.Clave = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        cancion.Titulo = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                        cancion.Dias = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        cancion.Activo = (!dataReader.IsDBNull(4)) ? dataReader.GetBoolean(4) : false;
                        cancion.AlAire = (!dataReader.IsDBNull(5)) ? dataReader.GetBoolean(5) : false;

                        canciones.Add(cancion);

                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción ArtistaDAO getCancionesReporteSinImagen():");
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
            return canciones;
        }
    }
}