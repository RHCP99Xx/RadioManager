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
    /// Lógica de interacción para IModificarCancion.xaml
    /// </summary>
    public partial class IModificarCancion : Window
    {
        Cancion cancionSeleccionada = new Cancion();
        string imagenSeleccionada = "";
        byte[] byteImagen = null;
        public IModificarCancion(Cancion cancion)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            cancionSeleccionada = cancion;
            llenarComboArtistas();
            llenarComboCategorias();
            llenarComboGeneros();
            llenarComboEstado();
            mostrarInformacionCancion();
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

        private void btnGuardarCancion_Click(object sender, RoutedEventArgs e)
        {
            if (txtTituloCancion.Text == "" || txtClaveCancion.Text == "")
            {
                txtTituloCancion.BorderBrush = Brushes.Red;
                txtClaveCancion.BorderBrush = Brushes.Red;
                MessageBox.Show("Favor de llenar todos los campos.");
            }
            else if (txtTituloCancion.Text.Length > 200)
            {
                MessageBox.Show("El título es demasiado largo.");
            }
            else if (txtClaveCancion.Text.Length > 10)
            {
                MessageBox.Show("La clave es demasiado larga.");
            }
            else
            {
                try
                {
                    CancionDAO cancionDao = new CancionDAO();
                    cancionSeleccionada.Titulo = txtTituloCancion.Text;
                    cancionSeleccionada.Clave = txtClaveCancion.Text;
                    cancionSeleccionada.IdCantante = ((Artista)comboArtista.SelectedValue).IdArtista;
                    cancionSeleccionada.IdGenero = ((Genero)comboGenero.SelectedValue).IdGenero;
                    cancionSeleccionada.IdCategoria = ((Categoria)comboCategoria.SelectedValue).IdCategoria;

                    if (byteImagen != null)
                    {
                        cancionSeleccionada.Imagen = byteImagen;
                    }

                    if (comboEstado.SelectedIndex == 0)
                    {
                        cancionSeleccionada.Activo = true;
                    } else if (comboEstado.SelectedIndex == 1)
                    {
                        cancionSeleccionada.Activo = false;
                    }

                    bool resultado = cancionDao.editarCancion(cancionSeleccionada);
                    if (resultado)
                    {
                        MessageBox.Show("¡Canción editada exitosamente!");
                        Owner.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error y no se pudo editar la canción, inténtelo de nuevo");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Ocurrió un error con la conexión a la base de datos", "ERROR");
                }
            }
        }

        private void btnSalirModificarCancion_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            this.Close();
        }
        private void btnSeleccionarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Multiselect = false;
            op.Title = "Selecciona una portada para la canción";
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
                imagenCancion.Fill = imageBrush;
            }
        }
        private void llenarComboArtistas()
        {
            ArtistaDAO artistaDAO = new ArtistaDAO();
            comboArtista.ItemsSource = artistaDAO.getArtistas();
        }
        private void llenarComboGeneros()
        {
            GeneroDAO generoDAO = new GeneroDAO();
            comboGenero.ItemsSource = generoDAO.obtenerGeneros();
        }
        private void llenarComboCategorias()
        {
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            comboCategoria.ItemsSource = categoriaDAO.obtenerCategorias();
        }
        private void llenarComboEstado()
        {
            List<string> listaEstado = new List<string>();
            listaEstado.Add("Activo");
            listaEstado.Add("Inactivo");
            comboEstado.ItemsSource = listaEstado;
        }
        private void mostrarInformacionCancion()
        {
            ArtistaDAO artistaDAO = new ArtistaDAO();
            Artista artista = artistaDAO.getArtistaPorId(cancionSeleccionada.IdCantante);

            GeneroDAO generoDAO = new GeneroDAO();
            Genero genero = generoDAO.obtenerGeneroPorId(cancionSeleccionada.IdGenero);

            CategoriaDAO categoriaDAO = new CategoriaDAO();
            Categoria categoria = categoriaDAO.obtenerCategoriaPorId(cancionSeleccionada.IdCategoria);

            txtTituloCancion.Text = cancionSeleccionada.Titulo;
            txtClaveCancion.Text = cancionSeleccionada.Clave;
            comboArtista.Text = artista.NombreArtistico;
            comboCategoria.Text = categoria.NombreCategoria;
            comboGenero.Text = genero.NombreGenero;

            if (cancionSeleccionada.Activo == true)
            {
                comboEstado.SelectedIndex = 0;
            } else if (cancionSeleccionada.Activo == false)
            {
                comboEstado.SelectedIndex = 1;
            }

            if (cancionSeleccionada.Imagen != null)
            {
                using (MemoryStream ms = new MemoryStream(cancionSeleccionada.Imagen))
                {
                    var imagen = new BitmapImage();
                    imagen.BeginInit();
                    imagen.StreamSource = ms;
                    imagen.CacheOption = BitmapCacheOption.OnLoad;
                    imagen.EndInit();

                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = imagen;
                    imagenCancion.Fill = imageBrush;
                }
            }
        }
    }
}
