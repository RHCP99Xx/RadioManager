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
    class ProgramaDAO
    {
        public List<Programa> getProgramas()
        {
            List<Programa> programas = new List<Programa>();
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand comando;
                    MySqlDataReader dataReader;
                    String query = "SELECT * FROM radio_manager.programa";
                    comando = new MySqlCommand(query, conn);
                    dataReader = comando.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Programa programa = new Programa();

                        programa.IdPrograma = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        programa.Nombre = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        programa.Activo = (!dataReader.IsDBNull(2)) ? dataReader.GetBoolean(2) : false;
                        programa.IdRadio = (!dataReader.IsDBNull(3)) ? dataReader.GetInt32(3) : 0;

                        programas.Add(programa);
                    }
                    dataReader.Close();
                    comando.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return programas;
        }

        public Programa getProgramaPorId(int idPrograma)
        {
            Programa programa = new Programa();
            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand comando;
                    MySqlDataReader dataReader;
                    String query = String.Format("SELECT * FROM programa WHERE idPrograma = '{0}';", idPrograma);
                    comando = new MySqlCommand(query, conn);
                    dataReader = comando.ExecuteReader();
                    while (dataReader.Read())
                    {
                        programa.IdPrograma = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        programa.Nombre = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        programa.Activo = (!dataReader.IsDBNull(2)) ? dataReader.GetBoolean(2) : false;
                        programa.IdRadio = (!dataReader.IsDBNull(3)) ? dataReader.GetInt32(3) : 0;
                    }
                    dataReader.Close();
                    comando.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return programa;
        }

        public Programa getProgramaPorNombre(String nombre)
        {
            Programa programa = new Programa();
            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand comando;
                    MySqlDataReader dataReader;
                    String query = String.Format("SELECT * FROM programa WHERE nombrePrograma = '{0}';", nombre);
                    comando = new MySqlCommand(query, conn);
                    dataReader = comando.ExecuteReader();
                    while (dataReader.Read())
                    {
                        programa.IdPrograma = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        programa.Nombre = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        programa.Activo = (!dataReader.IsDBNull(3)) ? dataReader.GetBoolean(3) : false;
                        programa.IdRadio = (!dataReader.IsDBNull(4)) ? dataReader.GetInt32(4) : 0;
                    }
                    dataReader.Close();
                    comando.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return programa;
        }

        public bool registrarPrograma(Programa programa)
        {
            bool registrado = false;

            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO radio_manager.programa VALUES(NULL, '" + programa.Nombre + "', '0', '1');", conn))
                    {
                        command.ExecuteNonQuery();
                    }
                    registrado = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        public bool editarPrograma(Programa programa)
        {
            bool editado = false;
            MySqlConnection conn = null;

            string estaActivo;
            if (programa.Activo == true)
            {
                estaActivo = "1";
            }
            else
            {
                estaActivo = "0";
            }

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    using (MySqlCommand command = new MySqlCommand("UPDATE `radio_manager`.`programa` SET `nombrePrograma` ='" + programa.Nombre + "', `activo` = '" + estaActivo + "'  WHERE `idPrograma` = " + programa.IdPrograma + ";", conn))
                    {
                        command.ExecuteNonQuery();
                    }
                    editado = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return editado;
        }

        public int obtenerIdPorNombre(String nombrePrograma)
        {
            Programa programa = new Programa();
            int idPrograma = 0;

            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand comando;
                    MySqlDataReader dataReader;
                    String query = String.Format("SELECT idPrograma FROM programa WHERE nombrePrograma = '{0}';", nombrePrograma);
                    comando = new MySqlCommand(query, conn);
                    dataReader = comando.ExecuteReader();
                    while (dataReader.Read())
                    {
                        programa.IdPrograma = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                    }

                    idPrograma = programa.IdPrograma;

                    dataReader.Close();
                    comando.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return idPrograma;
        }

        public bool activarConPatron(Programa programa)
        {
            bool activado = false;

            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    using (MySqlCommand command = new MySqlCommand("UPDATE `radio_manager`.`programa` SET `activo` ='1' WHERE `idPrograma` = " + programa.IdPrograma + ";", conn))
                    {
                        command.ExecuteNonQuery();
                    }
                    activado = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return activado;
        }
    }
}