using RadioManager.Models.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para ArtistInfo.xaml
    /// </summary>
    public partial class ArtistInfo : Window
    {
        Artista artistaSeleccionado = new Artista();
        public ObservableCollection<TablaCanciones> Canciones { get; set; } = new ObservableCollection<TablaCanciones>();

        public ArtistInfo(Artista artista)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            artistaSeleccionado = artista;
            mostrarInfoArtista();
        }

        public struct TablaCanciones
        {
            public int IdCancion { get; set; }
            public string Nombre { get; set; }
        }

        private void mostrarInfoArtista()
        {
            lblNombre.Content = artistaSeleccionado.NombreArtistico;

            if (!string.IsNullOrWhiteSpace(artistaSeleccionado.Descripcion))
            {
                textBlockDescripcion.Text = artistaSeleccionado.Descripcion;
            }
            else
            {
                textBlockDescripcion.Text = "No existe una descripión para este artista.";
            }
            

            if (artistaSeleccionado.Activo == true)
            {
                lblEstado.Content = "Activo";
            }
            else
            {
                lblEstado.Content = "Inactivo";
            }

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

        private void btn_Regresar_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            this.Close();
        }

        private void btn_Editar_Click(object sender, RoutedEventArgs e)
        {
            EditarArtista editar = new EditarArtista(artistaSeleccionado);
            editar.Owner = this;
            editar.Show();
            this.Hide();
        }
    }
}
