﻿using MySql.Data.MySqlClient;
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
                if(conn != null)
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }finally
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
                    String query = String.Format("SELECT * FROM cantante WHERE idPrograma = '{0}';", idPrograma);
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
                    String query = String.Format("SELECT * FROM cantante WHERE nombre = '{0}';", nombre);
                    comando = new MySqlCommand(query, conn);
                    dataReader = comando.ExecuteReader();
                    while (dataReader.Read())
                    {
                        programa.IdPrograma = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        programa.Nombre = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        programa.Activo = (!dataReader.IsDBNull(3)) ? dataReader.GetBoolean(3) : false;
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
    }
}