using Microsoft.Win32;
using RadioManager.Models.DAO;
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
    /// Lógica de interacción para IRegistrarCancion.xaml
    /// </summary>
    public partial class IRegistrarCancion : Window
    {
        string imagenSeleccionada = "";
        byte[] byteImagen = null;

        public IRegistrarCancion()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            llenarComboArtistas();
            llenarComboCategorias();
            llenarComboGeneros();
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
            if (string.IsNullOrWhiteSpace(txtTituloCancion.Text) || string.IsNullOrWhiteSpace(txtClaveCancion.Text))
            {
                txtTituloCancion.BorderBrush = Brushes.Red;
                txtClaveCancion.BorderBrush = Brushes.Red;
                MessageBox.Show("Favor de llenar todos los campos.");
            } else if (txtTituloCancion.Text.Length > 200)
            {
                MessageBox.Show("El título es demasiado largo.");
            } else if (txtClaveCancion.Text.Length > 10)
            {
                MessageBox.Show("La clave es demasiado larga.");
            }
            else
            {
                try
                {
                    CancionDAO cancionDao = new CancionDAO();
                    Cancion cancion = new Cancion();

                    cancion.Titulo = txtTituloCancion.Text;
                    cancion.Clave = txtClaveCancion.Text;
                    cancion.IdCantante = int.Parse(comboArtista.SelectedValue.ToString());
                    cancion.IdGenero = int.Parse(comboGenero.SelectedValue.ToString());
                    cancion.IdCategoria = int.Parse(comboCategoria.SelectedValue.ToString());
                    cancion.Imagen = byteImagen;

                    bool resultado = cancionDao.registrarCancion(cancion);
                    if (resultado)
                    {
                        MessageBox.Show("¡Canción registrada exitosamente!");
                        Owner.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error y no se pudo registrar la canción, inténtelo de nuevo");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Ocurrió un error con la conexión a la base de datos", "ERROR");
                }
            }
        }

        private void btnSalirRegistroCancion_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            this.Close();
        }

        private void btnSeleccionarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Multiselect = false;
            op.Title = "Selecciona una portada de la canción";
            op.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png)";

            if (op.ShowDialog() == true)
            {
                imagenSeleccionada = op.FileName;
                byteImagen = File.ReadAllBytes(imagenSeleccionada);
                FileInfo infoImagen = new FileInfo(imagenSeleccionada);

                if (infoImagen.Length > 64000)
                {
                    MessageBox.Show("La imagen supera los 50 KB");
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
    }
}
