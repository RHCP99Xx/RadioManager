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
    /// Lógica de interacción para EditarArtista.xaml
    /// </summary>
    public partial class EditarArtista : Window
    {
        Artista artistaSeleccionado = new Artista();
        string imagenSeleccionada = "";
        byte[] byteImagen = null;
        public EditarArtista(Artista artista)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            artistaSeleccionado = artista;
            mostrarInfoArtista();
            llenarCombobox();
        }

        private void mostrarInfoArtista()
        {
            txtNombre.Text = artistaSeleccionado.NombreArtistico;
            txtDescripcion.Text = artistaSeleccionado.Descripcion;

            if (artistaSeleccionado.Fotografia != null)
            {
                using (MemoryStream ms = new MemoryStream(artistaSeleccionado.Fotografia))
                {
                    var imagenArtista = new BitmapImage();
                    imagenArtista.BeginInit();
                    imagenArtista.StreamSource = ms;
                    imagenArtista.CacheOption = BitmapCacheOption.OnLoad;
                    imagenArtista.EndInit();

                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = imagenArtista;
                    ellipseFoto.Fill = imageBrush;
                }
            }
            else
            {
                iconoFoto.Visibility = Visibility.Visible;
            }
        }

        private void llenarCombobox()
        {
            List<string> listaFiltros = new List<string>();
            listaFiltros.Add("Activo");
            listaFiltros.Add("Inactivo");

            cmbEstado.ItemsSource = listaFiltros;
        }

        private void btn_Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text)
                )
            {
                this.txtNombre.BorderBrush = Brushes.Red;
                this.txtDescripcion.BorderBrush = Brushes.Red;
                MessageBox.Show("Favor de llenar todos los campos.");
            }
            else if (txtNombre.Text.Length > 200)
            {
                MessageBox.Show("El nombre es demasiado largo");
            }
            else if (txtDescripcion.Text.Length > 200)
            {
                MessageBox.Show("La descripción es demasiado larga");
            }
            else
            {
                try
                {
                    ArtistaDAO artistaDAO = new ArtistaDAO();

                    artistaSeleccionado.NombreArtistico = txtNombre.Text;
                    artistaSeleccionado.Descripcion = txtDescripcion.Text;

                    if (byteImagen != null)
                    {
                        artistaSeleccionado.Fotografia = byteImagen;
                    }

                    if (cmbEstado.SelectedIndex == 0)
                    {
                        artistaSeleccionado.Activo = true;
                    }else if (cmbEstado.SelectedIndex == 1)
                    {
                        artistaSeleccionado.Activo = false;
                    }

                    bool artistaEditado = artistaDAO.editarArtista(artistaSeleccionado);

                    if (artistaEditado)
                    {
                        MessageBox.Show("¡Los datos del artista han sido editados exitosamente!");
                        Owner.Owner.Show();
                        Owner.Close();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error y no se pudo modificar el artista, inténtelo de nuevo");
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

        private void btn_SubirImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Multiselect = false;
            op.Title = "Selecciona una fotografía del artista";
            op.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png)";

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
                iconoFoto.Visibility = Visibility.Hidden;
            }
        }
    }
}
