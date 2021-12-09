using MySql.Data.MySqlClient;
using RadioManager.Models.POCO;
using RadioManager.Models.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.DAO
{
    class CalendarioDAO
    {
        public List<Calendario> getCalendarios()
        {
            List<Calendario> calendarios = new List<Calendario>();
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand comando;
                    MySqlDataReader dataReader;
                    String query = "SELECT * FROM radio_manager.calendario";
                    comando = new MySqlCommand(query, conn);
                    dataReader = comando.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Calendario calendario = new Calendario();

                        calendario.IdPatron = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        calendario.IdPrograma = (!dataReader.IsDBNull(1)) ? dataReader.GetInt32(1) : 0;
                        calendario.CorteComercial = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                        calendario.Dia = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        calendario.HoraInicio = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
                        calendario.HoraFin = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";

                        calendarios.Add(calendario);
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

            return calendarios;
        }

        public Calendario getCalendarioPorId(int idCalendario)
        {
            Calendario calendario = new Calendario();
            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand comando;
                    MySqlDataReader dataReader;
                    String query = String.Format("SELECT * FROM calendario WHERE idCalendario = '{0}';", idCalendario);
                    comando = new MySqlCommand(query, conn);
                    dataReader = comando.ExecuteReader();
                    while (dataReader.Read())
                    {
                        calendario.IdPatron = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        calendario.IdPrograma = (!dataReader.IsDBNull(1)) ? dataReader.GetInt32(1) : 0;
                        calendario.CorteComercial = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                        calendario.Dia = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        calendario.HoraInicio = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
                        calendario.HoraFin = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
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
            return calendario;
        }

        public Calendario getCalendarioPorIdPrograma(int idPrograma)
        {
            Calendario calendario = new Calendario();
            MySqlConnection conn = null;

            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand comando;
                    MySqlDataReader dataReader;
                    String query = String.Format("SELECT * FROM calendario WHERE idPrograma = '{0}';", idPrograma);
                    comando = new MySqlCommand(query, conn);
                    dataReader = comando.ExecuteReader();
                    while (dataReader.Read())
                    {
                        calendario.IdPatron = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        calendario.IdPrograma = (!dataReader.IsDBNull(1)) ? dataReader.GetInt32(1) : 0;
                        calendario.CorteComercial = (!dataReader.IsDBNull(2)) ? dataReader.GetString(2) : "";
                        calendario.Dia = (!dataReader.IsDBNull(3)) ? dataReader.GetString(3) : "";
                        calendario.HoraInicio = (!dataReader.IsDBNull(4)) ? dataReader.GetString(4) : "";
                        calendario.HoraFin = (!dataReader.IsDBNull(5)) ? dataReader.GetString(5) : "";
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
            return calendario;
        }

        public bool registrarCalendario(Calendario calendario)
        {
            bool registrado = false;
            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO radio_manager.calendario VALUES('" + calendario.IdPatron + "', '" + calendario.IdPrograma + "', '" + calendario.CorteComercial + "', '" + calendario.Dia + "', '" + calendario.HoraInicio + "', '" + calendario.HoraFin + "');", conn))
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

        public bool editarCalendario(Calendario calendario)
        {
            bool editado = false;

            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    using (MySqlCommand command = new MySqlCommand("UPDATE `radio_manager`.`calendario` SET `idPatron` ='" + calendario.IdPatron + "', `corteComercial` ='" + calendario.CorteComercial + "', `dia` ='" + calendario.Dia + "', `horaInicio`='" + calendario.HoraInicio + "', `horaFIn`='" + calendario.HoraFin + "' WHERE `idPrograma` = " + calendario.IdPrograma + ";", conn))
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

        public bool asignarPatron(Calendario calendario)
        {
            bool asignado = false;

            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    using (MySqlCommand command = new MySqlCommand("UPDATE `radio_manager`.`calendario` SET `idPatron` ='" + calendario.IdPatron + "' WHERE `idPrograma` = " + calendario.IdPrograma + ";", conn))
                    {
                        command.ExecuteNonQuery();
                    }
                    asignado = true;
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

            return asignado;
        }
    }
}