using RadioManager.Models.POCO;
using System;
using System.Collections.Generic;
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

        public ArtistInfo(Artista artista)
        {
            InitializeComponent();
            artistaSeleccionado = artista;
            mostrarInfoArtista();
        }

        private void mostrarInfoArtista()
        {

        }

        private void btn_Regresar_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            this.Close();
        }
    }
}
