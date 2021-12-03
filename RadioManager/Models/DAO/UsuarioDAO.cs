using MySql.Data.MySqlClient;
using RadioManager.Models.BD;
using RadioManager.Models.POCO;
using System;
using System.Collections.Generic;
using RadioManager.Helpers.EncryptDataPackage;

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
                        "INNER JOIN radio_manager.creativo ON radio_manager.usuario.idUsuario = radio_manager.creativo.idUsuario ";
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
                        creativo.Username = (!reader.IsDBNull(9)) ? reader.GetString(9) : "";

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
                        "INNER JOIN radio_manager.operadordecabina ON radio_manager.usuario.idUsuario = radio_manager.operadordecabina.idUsuario ";
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
                        operador.NumPersonal = (!reader.IsDBNull(9)) ? reader.GetString(9) : "";

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


        public Operador getOperadorById(int id)
        {
            Operador operador = new Operador();

            MySqlConnection connection = null;
            try
            {
                connection = ConexionDB.getConnection();
                if (connection != null)
                {
                    MySqlDataReader reader;

                    string query = string.Format("SELECT operador.idUsuario, operador.numPersonal, usuario.nombre, usuario.apellidos, usuario.correo, usuario.contrasenia, usuario.activo " +
                        "FROM radio_manager.operadordecabina as operador " +
                        "INNER JOIN radio_manager.usuario as usuario ON radio_manager.operador.idUsuario = usuario.idUsuario " +
                        "WHERE operador.idUsuario = '{0}';", id);
                    MySqlCommand command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        operador.IdUsuario = (!reader.IsDBNull(0)) ? reader.GetInt32(0) : 0;
                        operador.NumPersonal = (!reader.IsDBNull(1)) ? reader.GetString(1) : "";
                        operador.Nombre = (!reader.IsDBNull(2)) ? reader.GetString(2) : "";
                        operador.Apellidos = (!reader.IsDBNull(3)) ? reader.GetString(3) : "";
                        operador.Correo = (!reader.IsDBNull(4)) ? reader.GetString(4) : "";
                        operador.Contrasenia = (!reader.IsDBNull(5)) ? reader.GetString(5) : "";
                        operador.Activo = (!reader.IsDBNull(6)) ? reader.GetBoolean(6) : false;

                    }
                    reader.Close();
                    command.Dispose();
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
            return operador;
        }

        public bool desactivarCreativo(int idCreativo)
        {
            bool isDiabled = false;
            MySqlConnection conn = null;
            try
            {
                string query = string.Format("UPDATE radio_manager.usuario SET activo = FALSE WHERE idUsuario=@idCreativo");
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@idCreativo", idCreativo);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    isDiabled = true;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return isDiabled;
        }

        public bool desactivarOperador(int idOperador)
        {
            bool isDiabled = false;
            MySqlConnection conn = null;
            try
            {
                string query = string.Format("UPDATE radio_manager.usuario SET activo = FALSE WHERE idUsuario=@idOperador");
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@idOperador", idOperador);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    isDiabled = true;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return isDiabled;
        }

        public bool activarCreativo(int idCreativo)
        {
            bool isEnabled = false;
            MySqlConnection conn = null;
            try
            {
                string query = string.Format("UPDATE radio_manager.usuario SET activo = TRUE WHERE idUsuario=@idCreativo");
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@idCreativo", idCreativo);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    isEnabled = true;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return isEnabled;
        }

        public bool activarOperador(int idOperador)
        {
            bool isEnabled = false;
            MySqlConnection conn = null;
            try
            {
                string query = string.Format("UPDATE radio_manager.usuario SET activo = TRUE WHERE idUsuario=@idOperador");
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.Parameters.AddWithValue("@idOperador", idOperador);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    isEnabled = true;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return isEnabled;
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
                    
                    string query = string.Format("SELECT creativo.idUsuario, creativo.userName, usuario.nombre, usuario.apellidos, usuario.correo, usuario.contrasenia, usuario.activo " +
                        "FROM radio_manager.creativo as creativo " +
                        "INNER JOIN radio_manager.usuario as usuario ON radio_manager.creativo.idUsuario = radio_manager.usuario.idUsuario " +
                        "WHERE radio_manager.creativo.idUsuario = '{0}';", id);
                    MySqlCommand command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        creativo.IdUsuario = (!reader.IsDBNull(0)) ? reader.GetInt32(0) : 0;
                        creativo.Username = (!reader.IsDBNull(1)) ? reader.GetString(1) : "";
                        creativo.Nombre = (!reader.IsDBNull(2)) ? reader.GetString(2) : "";
                        creativo.Apellidos = (!reader.IsDBNull(3)) ? reader.GetString(3) : "";
                        creativo.Correo = (!reader.IsDBNull(4)) ? reader.GetString(4) : "";
                        creativo.Contrasenia = (!reader.IsDBNull(5)) ? reader.GetString(5) : "";
                        creativo.Activo = (!reader.IsDBNull(6)) ? reader.GetBoolean(6) : false;
                        
                    }
                    reader.Close();
                    command.Dispose();
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
                }
                else
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

        public bool comprobarExistenciaCreativo(string username)
        {
            bool existeCreativo = false;
            MySqlConnection connection = null;

            try
            {
                connection = ConexionDB.getConnection();
                if (connection != null)
                {
                    MySqlDataReader reader = null;
                    string query = string.Format("SELECT creativo.username FROM radio_manager.creativo WHERE creativo.username = '{0}';", username);
                    MySqlCommand command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Creativo creativo = new Creativo();
                        creativo.Username = (!reader.IsDBNull(0)) ? reader.GetString(0) : "";
                        if (creativo.Username.Equals(username))
                        {
                            existeCreativo = true;
                        }
                        else
                        {
                            existeCreativo = false;
                        }
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
            return existeCreativo;
        }

        public bool comprobarExistenciaOperador(string numPersonal)
        {
            bool existeOperador = false;
            MySqlConnection connection = null;

            try
            {
                connection = ConexionDB.getConnection();
                if (connection != null)
                {
                    MySqlDataReader reader = null;
                    string query = string.Format("SELECT operadordecabina.numPersonal FROM radio_manager.operadordecabina WHERE operadordecabina.numPersonal = '{0}';", numPersonal);
                    MySqlCommand command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Operador operador = new Operador();
                        operador.NumPersonal = (!reader.IsDBNull(0)) ? reader.GetString(0) : "";
                        if (operador.NumPersonal.Equals(numPersonal))
                        {
                            existeOperador = true;
                        }
                        else
                        {
                            existeOperador = false;
                        }
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
            return existeOperador;
        }

        public bool editarCreativo(Creativo creativo)
        {
            bool creativoEditado = false;

            MySqlConnection connection = null;
            try
            {
                connection = ConexionDB.getConnection();
                string queryUsuario = String.Format("UPDATE radio_manager.usuario as usuario " +
                    "INNER JOIN radio_manager.creativo as creativo ON creativo.idUsuario = usuario.idUsuario SET usuario.nombre = @nombre, usuario.apellidos = @apellidos, " +
                    "usuario.correo = @correo, usuario.contrasenia = @contrasenia, creativo.userName = @username " +
                    "WHERE creativo.idUsuario = @idUsuario;");

                if (connection != null)
                {
                    MySqlCommand commandUsuario;
                    commandUsuario = new MySqlCommand(queryUsuario, connection);
                    commandUsuario.Parameters.AddWithValue("@nombre", creativo.Nombre);
                    commandUsuario.Parameters.AddWithValue("@apellidos", creativo.Apellidos);
                    commandUsuario.Parameters.AddWithValue("@correo", creativo.Correo);
                    commandUsuario.Parameters.AddWithValue("@contrasenia", creativo.Contrasenia);
                    commandUsuario.Parameters.AddWithValue("@username", creativo.Username);
                    commandUsuario.Parameters.AddWithValue("@idUsuario", creativo.IdUsuario);
                    commandUsuario.ExecuteNonQuery();

                    creativoEditado = true;
                }
                else
                {
                    creativoEditado = false;
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
            return creativoEditado;
        }
        public bool editarOperador(Operador operador)
        {
            bool operadorEditado = false;

            MySqlConnection connection = null;
            try
            {
                connection = ConexionDB.getConnection();
                string queryUsuario = String.Format("UPDATE radio_manager.usuario as usuario " +
                    "INNER JOIN radio_manager.operadordecabina as operador ON operador.idUsuario = usuario.idUsuario SET usuario.nombre = @nombre, usuario.apellidos = @apellidos, " +
                    "usuario.correo = @correo, usuario.contrasenia = @contrasenia, operador.numPersonal = @numPersonal " +
                    "WHERE operador.idUsuario = @idUsuario;");

                if (connection != null)
                {
                    MySqlCommand commandUsuario = new MySqlCommand();
                    commandUsuario.CommandText = queryUsuario;
                    commandUsuario.Parameters.AddWithValue("@nombre", operador.Nombre);
                    commandUsuario.Parameters.AddWithValue("@apellidos", operador.Apellidos);
                    commandUsuario.Parameters.AddWithValue("@correo", operador.Correo);
                    commandUsuario.Parameters.AddWithValue("@contrasenia", operador.Contrasenia);
                    commandUsuario.Parameters.AddWithValue("@numPersonal", operador.NumPersonal);
                    commandUsuario.Parameters.AddWithValue("@idUsuario", operador.IdUsuario);
                    commandUsuario.Connection = connection;
                    commandUsuario.ExecuteNonQuery();
                    operadorEditado = true;
                }
                else
                {
                    operadorEditado = false;
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
            return operadorEditado;
        }

        public bool login(string email, string password)
        {
            bool isLogged = false;
            Usuario usuario = new Usuario();
            MySqlConnection connection = null;

            try
            {
                connection = ConexionDB.getConnection();
                MySqlDataReader reader = null;

                if (connection != null)
                {
                    string query = string.Format("SELECT * FROM radio_manager.usuario WHERE usuario.correo = '{0}';", email);
                    MySqlCommand command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        usuario.Correo = (!reader.IsDBNull(3)) ? reader.GetString(3) : "";
                        usuario.Contrasenia = (!reader.IsDBNull(4)) ? reader.GetString(4) : "";
                    }
                    if (EncryptData.Decrypt(usuario.Contrasenia).Equals(password))
                    {
                        isLogged = true;
                    }
                    else
                    {
                        isLogged = false;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                isLogged = false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return isLogged;
        }
        
        public Creativo esCreativoLogin(string email)
        {
            Creativo creativo = new Creativo();
            MySqlConnection connection = null;
            try
            {
                connection = ConexionDB.getConnection();
                if (connection != null)
                {
                    MySqlDataReader reader = null;
                    
                    string query = string.Format("SELECT * FROM radio_manager.usuario " +
                        "INNER JOIN radio_manager.creativo " +
                        "ON radio_manager.usuario.idUsuario = radio_manager.creativo.idUsuario " +
                        "WHERE usuario.correo = '{0}';", email);
                    MySqlCommand command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        creativo.IdUsuario = (!reader.IsDBNull(0)) ? reader.GetInt32(0) : 0;
                        creativo.Nombre = (!reader.IsDBNull(1)) ? reader.GetString(1) : "";
                        creativo.Apellidos = (!reader.IsDBNull(2)) ? reader.GetString(2) : "";
                        creativo.Correo = (!reader.IsDBNull(3)) ? reader.GetString(3) : "";
                        creativo.Activo = (!reader.IsDBNull(5)) ? reader.GetBoolean(5) : false;
                        creativo.Username = (!reader.IsDBNull(9)) ? reader.GetString(9) : "";
                    }
                    reader.Close();
                    command.Dispose();
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
        public Operador esOperadorLogin(string email)
        {
            Operador operador = new Operador();
            MySqlConnection connection = null;
            try
            {
                connection = ConexionDB.getConnection();
                if (connection != null)
                {
                    MySqlDataReader reader = null;

                    string query = string.Format("SELECT * FROM radio_manager.usuario " +
                        "INNER JOIN radio_manager.operadordecabina " +
                        "ON radio_manager.usuario.idUsuario = radio_manager.operadordecabina.idUsuario " +
                        "WHERE usuario.correo = '{0}';", email);
                    MySqlCommand command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        operador.IdUsuario = (!reader.IsDBNull(0)) ? reader.GetInt32(0) : 0;
                        operador.Nombre = (!reader.IsDBNull(1)) ? reader.GetString(1) : "";
                        operador.Apellidos = (!reader.IsDBNull(2)) ? reader.GetString(2) : "";
                        operador.Correo = (!reader.IsDBNull(3)) ? reader.GetString(3) : "";
                        operador.Activo = (!reader.IsDBNull(5)) ? reader.GetBoolean(5) : false;
                        operador.NumPersonal = (!reader.IsDBNull(9)) ? reader.GetString(9) : "";
                    }
                    reader.Close();
                    command.Dispose();
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
            return operador;
        }
        public Usuario esAdminLogin(string email)
        {
            Usuario usuario = new Usuario();
            MySqlConnection connection = null;
            try
            {
                connection = ConexionDB.getConnection();
                if (connection != null)
                {
                    MySqlDataReader reader = null;

                    string query = string.Format("SELECT * FROM radio_manager.usuario " +
                        "WHERE usuario.correo = '{0}';", email);
                    MySqlCommand command = new MySqlCommand(query, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        usuario.IdUsuario = (!reader.IsDBNull(0)) ? reader.GetInt32(0) : 0;
                        usuario.Nombre = (!reader.IsDBNull(1)) ? reader.GetString(1) : "";
                        usuario.Apellidos = (!reader.IsDBNull(2)) ? reader.GetString(2) : "";
                        usuario.Correo = (!reader.IsDBNull(3)) ? reader.GetString(3) : "";
                        usuario.Activo = (!reader.IsDBNull(5)) ? reader.GetBoolean(5) : false;
                    }
                    reader.Close();
                    command.Dispose();
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
            return usuario;
        }
    }
}