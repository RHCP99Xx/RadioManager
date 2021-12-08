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
    class PatronDAO
    {
        public void guardarPatron(Patron patron)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "INSERT INTO radio_manager.patron(nombrePatron, activo) " +
                        "VALUES('" + patron.Nombre + "', " + patron.Activo + ")";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();

                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción PatronDAO guardarPatron(Patron patron):");
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

        public int getIdUltimoPatron()
        {
            int idPatron = 0;
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT MAX(idPatron) as ID FROM radio_manager.patron;";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        idPatron = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción PatronDAO getIdUltimoPatron()):");
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

            return idPatron;
        }

        public List<Patron> getTodoPatrones()
        {
            List<Patron> patrones = new List<Patron>();
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "SELECT idPatron, nombrePatron, activo FROM radio_manager.patron WHERE activo = TRUE;";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Patron patron = new Patron();

                        patron.IdPatron = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        patron.Nombre = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        patron.Activo = (!dataReader.IsDBNull(2)) ? dataReader.GetBoolean(2) : false;

                        patrones.Add(patron);
                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción PatronDAO getIdUltimoPatron()):");
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

            return patrones;
        }

        public void desactivarPatron(Patron patron)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "UPDATE radio_manager.patron SET activo = false WHERE idPatron = " + patron.IdPatron +";";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();

                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción PatronDAO desactivarPatron:");
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



        public void actualizarNombrePatron(Patron patron)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command;
                    MySqlDataReader dataReader;
                    String query = "UPDATE radio_manager.patron SET nombrePatron = '" + patron.Nombre + "' WHERE idPatron = " + patron.IdPatron + ";";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();

                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción PatronDAO actualizarNombrePatron(Patron patron):");
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
    }
}
