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
    /// Lógica de interacción para IConsultarCancion.xaml
    /// </summary>
    public partial class IConsultarCancion : Window
    {
        Cancion cancionSeleccionada = new Cancion();
        public IConsultarCancion(Cancion cancion)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            cancionSeleccionada = cancion;
            mostrarInformacionCancion();
        }

        private void mostrarInformacionCancion()
        {
            lblTituloCancion.Content = cancionSeleccionada.Titulo;
            lblClaveCancion.Content = cancionSeleccionada.Clave;
            lblCategoriaCancion.Content = cancionSeleccionada.NombreCategoria;
            lblGeneroCancion.Content = cancionSeleccionada.NombreGenero;
            lblArtistaCancion.Content = cancionSeleccionada.NombreArtista;

            if (cancionSeleccionada.Activo == true)
            {
                lblEstadoCancion.Content = "Activo";
            }
            else
            {
                lblEstadoCancion.Content = "Inactivo";
            }

            if (cancionSeleccionada.Dias.Equals("1234567"))
            {
                lblDiasCancion.Content = "Todos los días";
            }
            else
            {
                string cadenaDias = "";
                if (cancionSeleccionada.Dias.Contains("1"))
                {
                    cadenaDias += "Lunes ";
                }
                if (cancionSeleccionada.Dias.Contains("2"))
                {
                    cadenaDias += " Martes";
                }
                if (cancionSeleccionada.Dias.Contains("3"))
                {
                    cadenaDias += " Miércoles";
                }
                if (cancionSeleccionada.Dias.Contains("4"))
                {
                    cadenaDias += " Jueves";
                }
                if (cancionSeleccionada.Dias.Contains("5"))
                {
                    cadenaDias += " Viernes";
                }
                if (cancionSeleccionada.Dias.Contains("6"))
                {
                    cadenaDias += " Sábado";
                }
                if (cancionSeleccionada.Dias.Contains("7"))
                {
                    cadenaDias += " Domingo";
                }
                lblDiasCancion.Content = cadenaDias;
            }

            if (cancionSeleccionada.Imagen != null)
            {
                using (MemoryStream ms = new MemoryStream(cancionSeleccionada.Imagen))
                {
                    var caratulaCancion = new BitmapImage();
                    caratulaCancion.BeginInit();
                    caratulaCancion.StreamSource = ms;
                    caratulaCancion.CacheOption = BitmapCacheOption.OnLoad;
                    caratulaCancion.EndInit();

                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = caratulaCancion;
                    imagenCancion.Fill = imageBrush;
                }
            }
            else
            {
                iconoSinFoto.Visibility = Visibility.Visible;
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnArtista_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPatrones_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModificarCancion_Click(object sender, RoutedEventArgs e)
        {
            IModificarCancion modificarCancion = new IModificarCancion(cancionSeleccionada);
            modificarCancion.Owner = this;
            modificarCancion.Show();
            this.Hide();
        }

        private void btnSalirConsultar_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            this.Close();
        }
    }
}
