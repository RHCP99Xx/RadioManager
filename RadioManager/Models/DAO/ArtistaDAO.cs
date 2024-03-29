﻿using MySql.Data.MySqlClient;
using RadioManager.Models.BD;
using RadioManager.Models.POCO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                    MySqlDataReader dataReader;
                    String query = String.Format("SELECT * FROM cantante WHERE nombreArtistico = '{0}';", nombre);
                    MySqlCommand command = new MySqlCommand(query, conn);
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

                Artista artistaRepetido = getArtistaPorNombre(artista.NombreArtistico);

                if (conn != null)
                {
                    if (!artistaRepetido.Equals(artistaRepetido))
                    {
                        using (MySqlCommand command = new MySqlCommand("INSERT INTO radio_manager.cantante VALUES(NULL, '" + artista.NombreArtistico + "', " + "@data" + ", '" + artista.Descripcion + "', TRUE);", conn))
                        {
                            command.Parameters.AddWithValue("@data", artista.Fotografia);
                            command.ExecuteNonQuery();
                        }

                        registrado = true;
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un artista con ese nombre.");
                    }
                    
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

        public bool editarArtista(Artista artista)
        {
            bool artistaEditado = false;

            MySqlConnection conn = null;
            try
            {
                conn = ConexionDB.getConnection();
                if (conn != null)
                {
                    using (MySqlCommand command = new MySqlCommand("UPDATE `radio_manager`.`cantante` SET `nombreArtistico` = '" + artista.NombreArtistico + "', `fotografia` = " + "@data" + ", `descripcion` = '" + artista.Descripcion + "', `activo` = " + artista.Activo + " WHERE `idCantante` = " + artista.IdArtista + ";", conn))
                        {
                            command.Parameters.AddWithValue("@data", artista.Fotografia);
                            command.ExecuteNonQuery();
                    }

                    artistaEditado = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción ArtisTaDAO editarArtista");
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

            return artistaEditado;
        }

        //

        public List<Artista> getArtistasSinImagen()
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
                    String query = "SELECT idCantante, nombreArtistico FROM radio_manager.cantante;;";
                    command = new MySqlCommand(query, conn);
                    dataReader = command.ExecuteReader();


                    while (dataReader.Read())
                    {
                        Artista artista = new Artista();

                        artista.IdArtista = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        artista.NombreArtistico = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";


                        artistas.Add(artista);

                    }
                    dataReader.Close();
                    command.Dispose();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nExcepción ArtistaDAO getArtistasSinImagen():");
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

    }
}
