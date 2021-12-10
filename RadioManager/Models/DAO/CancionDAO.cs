using MySql.Data.MySqlClient;
using RadioManager.Models.BD;
using RadioManager.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.DAO
{
    class CancionDAO
    {
        public List<Cancion> obtenerCanciones()
        {
            List<Cancion> listaCanciones = new List<Cancion>();
            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT cancion.*, categoria.nombreCategoria, genero.nombreGenero, cantante.nombreArtistico FROM cancion " +
                        "LEFT JOIN categoria ON cancion.idCategoria = categoria.idCategoria " +
                        "LEFT JOIN genero ON cancion.isGenero = genero.idGenero " +
                        "LEFT JOIN cantante ON cancion.idCantante = cantante.idCantante;";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Cancion cancion = new Cancion();

                        cancion.IdCancion = !dataReader.IsDBNull(0) ? dataReader.GetInt32(0) : 0;
                        cancion.Titulo = !dataReader.IsDBNull(1) ? dataReader.GetString(1) : "";
                        cancion.IdCategoria = !dataReader.IsDBNull(2) ? dataReader.GetInt32(2) : 0;
                        cancion.IdGenero = !dataReader.IsDBNull(3) ? dataReader.GetInt32(3) : 0;
                        cancion.Clave = !dataReader.IsDBNull(4) ? dataReader.GetString(4) : "";
                        cancion.Dias = !dataReader.IsDBNull(5) ? dataReader.GetString(5) : "";
                        cancion.IdCantante = !dataReader.IsDBNull(6) ? dataReader.GetInt32(6) : 0;
                        cancion.Activo = !dataReader.IsDBNull(7) ? dataReader.GetBoolean(7) : false;
                        cancion.AlAire = !dataReader.IsDBNull(8) ? dataReader.GetBoolean(8) : false;
                        cancion.Imagen = !dataReader.IsDBNull(9) ? (byte[]) dataReader.GetValue(9) : null;
                        
                        cancion.NombreCategoria = !dataReader.IsDBNull(10) ? dataReader.GetString(10) : "";
                        cancion.NombreGenero = !dataReader.IsDBNull(11) ? dataReader.GetString(11) : "";
                        cancion.NombreArtista = !dataReader.IsDBNull(12) ? dataReader.GetString(12) : "";

                        listaCanciones.Add(cancion);
                    }
                    dataReader.Close();
                    command.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción generada en CancionDAO.obtenerCanciones(): ");
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
            return listaCanciones;
        }

        public Cancion obtenerCancionPorID(int id)
        {
            Cancion cancion = new Cancion();
            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = String.Format("SELECT cancion.*, categoria.nombreCategoria, genero.nombreGenero, cantante.nombreArtistico FROM cancion " +
                        "LEFT JOIN categoria ON cancion.idCategoria = categoria.idCategoria " +
                        "LEFT JOIN genero ON cancion.isGenero = genero.idGenero " +
                        "LEFT JOIN cantante ON cancion.idCantante = cantante.idCantante " +
                        "WHERE cancion.idCancion = {0};", id);
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        cancion.IdCancion = !dataReader.IsDBNull(0) ? dataReader.GetInt32(0) : 0;
                        cancion.Titulo = !dataReader.IsDBNull(1) ? dataReader.GetString(1) : "";
                        cancion.IdCategoria = !dataReader.IsDBNull(2) ? dataReader.GetInt32(2) : 0;
                        cancion.IdGenero = !dataReader.IsDBNull(3) ? dataReader.GetInt32(3) : 0;
                        cancion.Clave = !dataReader.IsDBNull(4) ? dataReader.GetString(4) : "";
                        cancion.Dias = !dataReader.IsDBNull(5) ? dataReader.GetString(5) : "";
                        cancion.IdCantante = !dataReader.IsDBNull(6) ? dataReader.GetInt32(6) : 0;
                        cancion.Activo = !dataReader.IsDBNull(7) ? dataReader.GetBoolean(7) : false;
                        cancion.AlAire = !dataReader.IsDBNull(8) ? dataReader.GetBoolean(8) : false;
                        cancion.Imagen = !dataReader.IsDBNull(9) ? (byte[])dataReader.GetValue(9) : null;

                        cancion.NombreCategoria = !dataReader.IsDBNull(10) ? dataReader.GetString(10) : "";
                        cancion.NombreGenero = !dataReader.IsDBNull(11) ? dataReader.GetString(11) : "";
                        cancion.NombreArtista = !dataReader.IsDBNull(12) ? dataReader.GetString(12) : "";
                    }
                    dataReader.Close();
                    command.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción generada en CancionDAO.obtenerCancionPorID(): ");
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
            return cancion;
        }

        public List<Cancion> obtenerCancionPorTitulo(String titulo)
        {
            MySqlConnection conn = null;
            List<Cancion> listaCanciones = new List<Cancion>();

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = String.Format("SELECT cancion.*, categoria.nombreCategoria, genero.nombreGenero, cantante.nombreArtistico FROM cancion " +
                        "LEFT JOIN categoria ON cancion.idCategoria = categoria.idCategoria " +
                        "LEFT JOIN genero ON cancion.isGenero = genero.idGenero " +
                        "LEFT JOIN cantante ON cancion.idCantante = cantante.idCantante " +
                        "WHERE titulo = '{0}';", titulo);
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Cancion cancion = new Cancion();
                        cancion.IdCancion = !dataReader.IsDBNull(0) ? dataReader.GetInt32(0) : 0;
                        cancion.Titulo = !dataReader.IsDBNull(1) ? dataReader.GetString(1) : "";
                        cancion.IdCategoria = !dataReader.IsDBNull(2) ? dataReader.GetInt32(2) : 0;
                        cancion.IdGenero = !dataReader.IsDBNull(3) ? dataReader.GetInt32(3) : 0;
                        cancion.Clave = !dataReader.IsDBNull(4) ? dataReader.GetString(4) : "";
                        cancion.Dias = !dataReader.IsDBNull(5) ? dataReader.GetString(5) : "";
                        cancion.IdCantante = !dataReader.IsDBNull(6) ? dataReader.GetInt32(6) : 0;
                        cancion.Activo = !dataReader.IsDBNull(7) ? dataReader.GetBoolean(7) : false;
                        cancion.AlAire = !dataReader.IsDBNull(8) ? dataReader.GetBoolean(8) : false;
                        cancion.Imagen = !dataReader.IsDBNull(9) ? (byte[])dataReader.GetValue(9) : null;

                        cancion.NombreCategoria = !dataReader.IsDBNull(10) ? dataReader.GetString(10) : "";
                        cancion.NombreGenero = !dataReader.IsDBNull(11) ? dataReader.GetString(11) : "";
                        cancion.NombreArtista = !dataReader.IsDBNull(12) ? dataReader.GetString(12) : "";

                        listaCanciones.Add(cancion);
                    }
                    dataReader.Close();
                    command.Dispose();
                }
            } 
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción generada en CancionDAO.obtenerCancionPorTitulo(): ");
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
            return listaCanciones;
        }

        public List<Cancion> obtenerCancionesPorArtista(int idArtista)
        {
            List<Cancion> listaCanciones = new List<Cancion>();
            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT * FROM radio_manager.cancion WHERE idCantante = "+ idArtista + ";";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Cancion cancion = new Cancion();

                        cancion.IdCancion = !dataReader.IsDBNull(0) ? dataReader.GetInt32(0) : 0;
                        cancion.Titulo = !dataReader.IsDBNull(1) ? dataReader.GetString(1) : "";

                        listaCanciones.Add(cancion);
                    }
                    dataReader.Close();
                    command.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción generada en CancionDAO.obtenerCanciones(): ");
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
            return listaCanciones;
        }

        public bool registrarCancion(Cancion cancion)
        {
            bool resultado = false;
            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO radio_manager.cancion VALUES(NULL, '" + cancion.Titulo + "', '" 
                        + cancion.IdCategoria + "', '" + cancion.IdGenero + "', '" + cancion.Clave + "', NULL, '" + cancion.IdCantante + "', TRUE, FALSE, " + "@data" + ");", conn))
                    {
                        command.Parameters.AddWithValue("@data", cancion.Imagen);
                        command.ExecuteNonQuery();
                    }

                    resultado = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción generada en CancionDAO.registrarCancion(): ");
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
            return resultado;
        }

        public bool editarCancion(Cancion cancion)
        {
            Console.WriteLine(cancion.Clave);
            bool resultado = false;
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    using (MySqlCommand command = new MySqlCommand("UPDATE `radio_manager`.`cancion` SET `titulo` = '" + cancion.Titulo + "', `claves` = '" 
                        + cancion.Clave + "', `idCategoria` = " + cancion.IdCategoria + ", `isGenero` = " + cancion.IdGenero + ", `idCantante` = " 
                        + cancion.IdCantante + ", `activo` = " + cancion.Activo + ", `imagen` = " + "@data" + " WHERE `idCancion` = " + cancion.IdCancion + ";", conn))
                    {
                        command.Parameters.AddWithValue("@data", cancion.Imagen);
                        command.ExecuteNonQuery();
                    }

                    resultado = true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("\nExcepción generada en CancionDAO.editarCancion(): ");
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
            Console.WriteLine(cancion.Clave);
            return resultado;
        }

        //
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