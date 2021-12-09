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
    /// Lógica de interacción para ArtistMainWindow.xaml
    /// </summary>
    public partial class ArtistMainWindow : Window
    {
        public ObservableCollection<TablaArtistas> Artistas { get; set; } = new ObservableCollection<TablaArtistas>();
        public ArtistMainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataContext = this;
            mostrarArtistas();
        }

        public struct TablaArtistas
        {
            public int IdArtista { get; set; }
            public string Nombre { get; set; }
        }

        public void mostrarArtistas()
        {
            ArtistaDAO artistaDAO = new ArtistaDAO();
            var artistas = artistaDAO.getArtistas();

            Artistas.Clear();

            foreach (var artista in artistas)
            {
                Artistas.Add(new TablaArtistas
                {
                    IdArtista = artista.IdArtista,
                    Nombre = artista.NombreArtistico,
                });
            }
        }

        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                Artistas.Clear();
                mostrarArtistas();
            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
                {
                    ArtistaDAO artistaDAO = new ArtistaDAO();
                    var artista = artistaDAO.getArtistaPorNombre(txtBusqueda.Text);

                    if (!artista.Equals(null))
                    {
                        Artistas.Clear();
                        Artistas.Add(new TablaArtistas
                        {
                            IdArtista = artista.IdArtista,
                            Nombre = artista.NombreArtistico,
                        });
                    }
                    else
                    {
                        Artistas.Clear();
                        MessageBox.Show("No existe ningún artista con ese nombre");
                        mostrarArtistas();
                    }
                }
                else
                {
                    Artistas.Clear();
                    MessageBox.Show("Escriba el nombre de un artista");
                    mostrarArtistas();
                }
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            RegistrarArtista registrar = new RegistrarArtista();
            registrar.Owner = this;
            registrar.Show();
            this.Hide();
        }

        private void lvArtistas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var artistaSeleccionado = (TablaArtistas)lvArtistas.SelectedItem;
            ArtistaDAO artistaDAO = new ArtistaDAO();
            Artista artista = artistaDAO.getArtistaPorId(artistaSeleccionado.IdArtista);

            if (lvArtistas.SelectedItem != null)
            {
                ArtistInfo artistInfo = new ArtistInfo(artista);
                artistInfo.Owner = this;
                artistInfo.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Escoja un artista", "ERROR");
            }
        }

        private void btnPrincipal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
