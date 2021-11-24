using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using RadioManager.Models.BD;
using RadioManager.Models.POCO;

namespace RadioManager.Models.DAO
{
    public class UsuarioDAO
    {
        public List<Creativo> getCreativosActivos()
        {
            List<Creativo> creativos = new List<Creativo>();

            MySqlConnection connection = null;

            try
            {
                connection = ConexionDB.getConnection();
                if (connection != null)
                {
                    MySqlCommand command;
                    MySqlDataReader reader;
                    string query = "SELECT * FROM radio_manager.usuario " +
                        "INNER JOIN radio_manager.creativo ON radio_manager.usuario.idUsuario = radio_manager.creativo.idUsuario " +
                        "WHERE radio_manager.usuario.activo = 1;";
                    command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Creativo creativo = new Creativo();

                        creativo.IdUsuario = (!reader.IsDBNull(0)) ? reader.GetInt32(0) : 0;
                        creativo.Nombre = (!reader.IsDBNull(1)) ? reader.GetString(1) : "";
                        creativo.Apellidos = (!reader.IsDBNull(2)) ? reader.GetString(2) : "";
                        creativo.Correo = (!reader.IsDBNull(3)) ? reader.GetString(3) : "";
                        creativo.Contrasenia = (!reader.IsDBNull(4)) ? reader.GetString(4) : "";
                        creativo.Activo = (!reader.IsDBNull(5)) ? reader.GetBoolean(5) : false;
                        creativo.Username = (!reader.IsDBNull(8)) ? reader.GetString(8) : "";

                        creativos.Add(creativo);
                    }
                    reader.Close();
                    command.Dispose();
                }
                {

                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return creativos;
        }

        public List<Operador> getOperadoresActivos()
        {
            List<Operador> operadores = new List<Operador>();

            MySqlConnection connection = null;

            try
            {
                connection = ConexionDB.getConnection();
                if (connection != null)
                {
                    MySqlCommand command;
                    MySqlDataReader reader;
                    string query = "SELECT * FROM radio_manager.usuario " +
                        "INNER JOIN radio_manager.operadordecabina ON radio_manager.usuario.idUsuario = radio_manager.operadordecabina.idUsuario " +
                        "WHERE radio_manager.usuario.activo = 1;";
                    command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Operador operador = new Operador();

                        operador.IdUsuario = (!reader.IsDBNull(0)) ? reader.GetInt32(0) : 0;
                        operador.Nombre = (!reader.IsDBNull(1)) ? reader.GetString(1) : "";
                        operador.Apellidos = (!reader.IsDBNull(2)) ? reader.GetString(2) : "";
                        operador.Correo = (!reader.IsDBNull(3)) ? reader.GetString(3) : "";
                        operador.Contrasenia = (!reader.IsDBNull(4)) ? reader.GetString(4) : "";
                        operador.Activo = (!reader.IsDBNull(5)) ? reader.GetBoolean(5) : false;
                        operador.NumPersonal = (!reader.IsDBNull(8)) ? reader.GetString(8) : "";

                        operadores.Add(operador);
                    }
                    reader.Close();
                    command.Dispose();
                }
                {

                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return operadores;
        }

        public Creativo getCreativoById(int id)
        {
            Creativo creativo = new Creativo();

            MySqlConnection connection = null;
            try
            {
                connection = ConexionDB.getConnection();
                if (connection != null)
                {
                    MySqlDataReader reader;
                    MySqlCommand command;
                    String query = String.Format("SELECT * FROM radio_manager.usuario " +
                        "INNER JOIN radio_manager.creativo ON radio_manager.usuario.idUsuario = radio_manager.creativo.idUsuario " +
                        "WHERE radio_manager.creativo.idUsuario = '{0}';", id);
                    command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    while (true)
                    {
                        creativo.IdUsuario = (!reader.IsDBNull(0)) ? reader.GetInt32(0) : 0;
                        creativo.Nombre = (!reader.IsDBNull(1)) ? reader.GetString(1) : "";
                        creativo.Apellidos = (!reader.IsDBNull(2)) ? reader.GetString(2) : "";
                        creativo.Correo = (!reader.IsDBNull(3)) ? reader.GetString(3) : "";
                        creativo.Contrasenia = (!reader.IsDBNull(4)) ? reader.GetString(4) : "";
                        creativo.Activo = (!reader.IsDBNull(5)) ? reader.GetBoolean(5) : false;
                        creativo.Username = (!reader.IsDBNull(8)) ? reader.GetString(8) : "";
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return creativo;
        }


        //Este método permite al administrador el programa registrar un nuevo usuario como creativo
        public bool registrarUsuarioCreativo(Creativo creativo)
        {
            bool creativoRegistrado = false;

            MySqlConnection connection = null;
            try
            {
                connection = ConexionDB.getConnection();
                string queryUsuario = String.Format("INSERT INTO radio_manager.usuario(idUsuario, nombre, apellidos, correo, contrasenia, activo, idRadio) " +
                        "VALUES(NULL, @nombre, @apellidos, @correo, @contrasenia, TRUE, 1);");
                string queryCreativo = String.Format("INSERT INTO radio_manager.creativo(idUsuario, userName) VALUES (last_insert_id(), @userName);");
                if (connection != null)
                {
                    MySqlCommand command1;
                    command1 = new MySqlCommand(queryUsuario, connection);
                    command1.Parameters.AddWithValue("@nombre", creativo.Nombre);
                    command1.Parameters.AddWithValue("@apellidos", creativo.Apellidos);
                    command1.Parameters.AddWithValue("@correo", creativo.Correo);
                    command1.Parameters.AddWithValue("@contrasenia", creativo.Contrasenia);
                    command1.Parameters.AddWithValue("@activo", creativo.Activo);
                    command1.ExecuteNonQuery();
                    command1.Parameters.Clear();
                    MySqlCommand command2;
                    command2 = new MySqlCommand(queryCreativo, connection);
                    command2.Parameters.AddWithValue("@idUsuario", "last_insert_id()");
                    command2.Parameters.AddWithValue("@userName", creativo.Username);
                    command2.ExecuteNonQuery();
                    command2.Parameters.Clear();
                    creativoRegistrado = true;
                }else
                {
                    creativoRegistrado = false;
                }
            }
            catch (MySqlException e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return creativoRegistrado;
        }

        //Este método permite al administrador el programa registrar un nuevo usuario como operador de cabina
        public bool registrarUsuarioCabina(Operador operador)
        {
            bool cabinaRegistrado = false;
            MySqlConnection connection = null;
            try
            {
                connection = ConexionDB.getConnection();
                string queryUsuario = String.Format("INSERT INTO radio_manager.usuario(idUsuario, nombre, apellidos, correo, contrasenia, activo, idRadio) " +
                        "VALUES(NULL, @nombre, @apellidos, @correo, @contrasenia, TRUE, 1)");
                string queryOperador = String.Format("INSERT INTO radio_manager.operadordecabina(idUsuario, numPersonal) VALUES(last_insert_id(), @numPersonal);");

                if (connection != null)
                {
                    MySqlCommand command1;
                    command1 = new MySqlCommand(queryUsuario, connection);
                    command1.Parameters.AddWithValue("@nombre", operador.Nombre);
                    command1.Parameters.AddWithValue("@apellidos", operador.Apellidos);
                    command1.Parameters.AddWithValue("@correo", operador.Correo);
                    command1.Parameters.AddWithValue("@contrasenia", operador.Contrasenia);
                    command1.ExecuteNonQuery();
                    command1.Parameters.Clear();
                    MySqlCommand command2;
                    command2 = new MySqlCommand(queryOperador, connection);
                    command2.Parameters.AddWithValue("@numPersonal", operador.NumPersonal);
                    command2.ExecuteNonQuery();
                    command2.Parameters.Clear();
                    cabinaRegistrado = true;
                }
                else
                {
                    cabinaRegistrado = false;
                }
            }
            catch (MySqlException e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return cabinaRegistrado;
        }
    }
}
