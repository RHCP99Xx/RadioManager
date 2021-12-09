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
    class LineaPatronDAO
    {
        public void guardarLineaPatron(LineaPatron lineaPatron)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "INSERT INTO radio_manager.lineapatron (posicion, idCategoria, idGenero, idPatron)" +
                    "VALUES(" + lineaPatron.Posicion + ", " + lineaPatron.Categoria.IdCategoria + ", " + lineaPatron.Genero.IdGenero + ", " + lineaPatron.IdPatron + "); ";

                    Console.WriteLine(query);

                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();

                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción LineaPatronDAO guardarLineaPatron(LineaPatron LineaPatron):");
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
        }


        public List<LineaPatron> getLineasPatron(Patron patron)
        {
            List<LineaPatron> lineasPatron = new List<LineaPatron>();

            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT idLineaPatron, posicion, categoria.idCategoria, nombreCategoria, genero.idGenero, nombreGenero, idPatron " +
                                    "FROM radio_manager.lineapatron " +
                                    "INNER JOIN radio_manager.categoria " +
                                    "ON lineapatron.idCategoria = categoria.idCategoria " +
                                    "INNER JOIN radio_manager.genero " +
                                    "ON lineapatron.idGenero = genero.idGenero " +
                                    "WHERE idPatron = " + patron.IdPatron + ";";

                    Console.WriteLine(query);

                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        LineaPatron lineaPatron = new LineaPatron();
                        Categoria categoria = new Categoria();
                        Genero genero = new Genero();

                        lineaPatron.IdLineaPatron = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        lineaPatron.Posicion = (!dataReader.IsDBNull(1)) ? dataReader.GetInt32(1) : 0;

                        categoria.IdCategoria = (!dataReader.IsDBNull(2)) ? dataReader.GetInt32(2) : 0;
                        categoria.NombreCategoria = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        lineaPatron.Categoria = categoria;

                        genero.IdGenero = (!dataReader.IsDBNull(4)) ? dataReader.GetInt32(4) : 0;
                        genero.NombreGenero = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
                        lineaPatron.Genero = genero;

                        lineaPatron.IdPatron = (!dataReader.IsDBNull(6)) ? dataReader.GetInt32(6) : 0;

                        lineasPatron.Add(lineaPatron);
                    }


                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción LineaPatronDAO guardarLineaPatron(LineaPatron LineaPatron):");
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

            return lineasPatron;
        }


        public void eliminarLineaPatron(Patron patron)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "DELETE FROM radio_manager.lineapatron WHERE idPatron = " + patron.IdPatron + " ;";

                    Console.WriteLine(query);

                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();

                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción LineaPatronDAO eliminarLineaPatron(Patron patron):");
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
        }


        public List<LineaPatron> getLineasPatronUsadas()
        {
            List<LineaPatron> lineasPatron = new List<LineaPatron>();
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT idCategoria, idGenero " +
                                    "FROM radio_manager.lineapatron " +
                                    "GROUP BY idCategoria, idGenero;";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();


                    while (dataReader.Read())
                    {
                        LineaPatron lineaPatron = new LineaPatron();
                        lineaPatron.Categoria = new Categoria();
                        lineaPatron.Genero = new Genero();
                        lineaPatron.Categoria.IdCategoria = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        lineaPatron.Genero.IdGenero = (!dataReader.IsDBNull(1)) ? dataReader.GetInt32(1) : 0;

                        lineasPatron.Add(lineaPatron);

                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción LineaPatronDAO getLineasPatronUsadas():");
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
            return lineasPatron;
        }


        public List<LineaPatronReporte> getLineasPatronSinUsar()
        {
            List<LineaPatron> lineasPatronUsadas  = getLineasPatronUsadas();
            List<LineaPatronReporte> lineasPatron = new List<LineaPatronReporte>();

            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT nombreCategoria, nombreGenero, COUNT(isGenero) AS numCanciones FROM radio_manager.cancion " +
                                    "INNER JOIN radio_manager.categoria ON cancion.idCategoria = categoria.idCategoria " +
                                    "INNER JOIN radio_manager.genero ON genero.idGenero WHERE ";
                    int posicion = 1;
                    foreach (LineaPatron linea in lineasPatronUsadas)
                    {
                        if( posicion < (lineasPatronUsadas.Count))
                        {
                            query = query + "(cancion.idCategoria != " + linea.Categoria.IdCategoria + " AND isGenero != " + linea.Genero.IdGenero + ") AND ";
                        }
                        else
                        {
                            query = query + "(cancion.idCategoria != " + linea.Categoria.IdCategoria + " AND isGenero != " + linea.Genero.IdGenero + ") ";
                        }
                        
                        posicion++;
                    }
                    query = query + "GROUP BY nombreCategoria, nombreGenero;";


                    String queryFinal = query;
                    Console.WriteLine(queryFinal);

                    command = new MySqlCommand(queryFinal, conn);
                   dataReader = command.ExecuteReader();

                    

                    while (dataReader.Read())
                    {
                        LineaPatronReporte lineaPatronReporte = new LineaPatronReporte();
                        lineaPatronReporte.Categoria = new Categoria();
                        lineaPatronReporte.Genero = new Genero();

                        lineaPatronReporte.Categoria.NombreCategoria = (!dataReader.IsDBNull(0)) ? dataReader.GetString(0) : "";
                        lineaPatronReporte.Genero.NombreGenero = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        lineaPatronReporte.NumCanciones = (!dataReader.IsDBNull(2)) ? dataReader.GetInt32(2) : 0;


                        lineasPatron.Add(lineaPatronReporte);

                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción LineaPatronDAO getLineasPatronSinUsar():");
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
            return lineasPatron;
        }

    }
}
