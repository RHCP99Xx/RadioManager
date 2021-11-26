using RadioManager.Models.DAO;
using RadioManager.Models.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para IMenuCanciones.xaml
    /// </summary>
    public partial class IMenuCanciones : Window
    {
        public ObservableCollection<Cancion> Canciones { get; set; } = new ObservableCollection<Cancion>();
        public IMenuCanciones()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            mostrarCanciones();
        }

        /*public struct TablaCanciones
        {
            public int IdCancion { get; set; }
            public string Titulo { get; set; }
        }*/

        public void mostrarCanciones()
        {
            CancionDAO cancionDao = new CancionDAO();
            var canciones = cancionDao.obtenerCanciones();

            Canciones.Clear();

            foreach (var cancion in canciones)
            {
                Canciones.Add(cancion);
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

        private void btnAgregarCancion_Click(object sender, RoutedEventArgs e)
        {
            IRegistrarCancion registrarCancion = new IRegistrarCancion();
            registrarCancion.Owner = this;
            registrarCancion.Show();
            this.Hide();
        }

        private void btnCancionAlAire_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtBuscarCancion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarCancion.Text))
            {
                Canciones.Clear();
                mostrarCanciones();
            }
        }

        private void txtBuscarCancion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtBuscarCancion.Text))
                {
                    CancionDAO cancionDao = new CancionDAO();
                    List<Cancion> lista = cancionDao.obtenerCancionPorTitulo(txtBuscarCancion.Text);

                    Canciones.Clear();
                    if (lista.Count > 0)
                    { 
                        foreach (Cancion cancion in lista)
                        {
                            Canciones.Add(cancion);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un título");
                }
            }
        }

        private void listaCanciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cancion cancionSeleccionada = (Cancion) listaCanciones.SelectedItem;

            if (listaCanciones.SelectedItem != null)
            {
                IConsultarCancion consultarCancion = new IConsultarCancion(cancionSeleccionada);
                consultarCancion.Owner = this;
                consultarCancion.Show();
                this.Hide();
            }
        }
    }
}
