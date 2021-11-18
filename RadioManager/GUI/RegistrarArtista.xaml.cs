using Microsoft.Win32;
using RadioManager.Models.DAO;
using RadioManager.Models.POCO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RadioManager.GUI
{
    /// <summary>
    /// Lógica de interacción para RegistrarArtista.xaml
    /// </summary>
    public partial class RegistrarArtista : Window
    {
        string imagenSeleccionada = "";
        byte[] byteImagen = null;


        public RegistrarArtista()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btn_SubirImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Multiselect = false;
            op.Title = "Selecciona una fotografía del artista";
            op.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (op.ShowDialog() == true)
            {
                imagenSeleccionada = op.FileName;
                byteImagen = File.ReadAllBytes(imagenSeleccionada);
                FileInfo infoImagen = new FileInfo(imagenSeleccionada);

                if (infoImagen.Length > 64000)
                {
                    MessageBox.Show("La imagen excede los 50 KB");
                }
            }

            if (imagenSeleccionada.Length > 0)
            {
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage(new Uri(imagenSeleccionada));
                ellipseFoto.Fill = imageBrush;
            }
        }

        private void registrarArtista()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text)
                )
            {
                this.txtNombre.BorderBrush = Brushes.Red;
                this.txtDescripcion.BorderBrush = Brushes.Red;
                MessageBox.Show("Favor de llenar todos los campos.");
            }else if(txtNombre.Text.Length > 200){
                MessageBox.Show("El nombre es demasiado largo");
            }else if(txtDescripcion.Text.Length > 200){
                MessageBox.Show("La descripción es demasiado larga");
            }else
            {
                try
                {
                    ArtistaDAO artistaDAO = new ArtistaDAO();

                    string nombre = txtNombre.Text;
                    string descripcion = txtDescripcion.Text;

                    Artista artista = new Artista();
                    artista.NombreArtistico = nombre;
                    artista.Descripcion = descripcion;
                    artista.Fotografia = byteImagen;
                    artista.Activo = true;

                    bool artistaRegistrado = artistaDAO.registrarArtista(artista);

                    if (artistaRegistrado)
                    {
                        MessageBox.Show("¡Artista registrado exitosamente!");
                        Owner.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error y no se pudo registrar el artista, inténtelo de nuevo");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ocurrió un error con la conexión a la base de datos", "ERROR");
                }
            }
        }

        private void btn_Regresar_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            this.Close();
        }

        private void btn_Guardar_Click(object sender, RoutedEventArgs e)
        {
            registrarArtista();
        }
    }
}
