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
    /// Lógica de interacción para IModificarCancionesAlAire.xaml
    /// </summary>
    public partial class IConsultarCancionesAlAire : Window
    {
        public ObservableCollection<Cancion> Canciones { get; set; } = new ObservableCollection<Cancion>();
        public IConsultarCancionesAlAire()
        {
            InitializeComponent();
            DataContext = this;
            mostrarCancionesFuera();
        }

        public void mostrarCancionesFuera()
        {
            CancionDAO cancionDao = new CancionDAO();
            var canciones = cancionDao.obtenerCancionesFueraAire();

            Canciones.Clear();

            foreach (var cancion in canciones)
            {
                Canciones.Add(cancion);
            }
        }

        private void listaCanciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cancion cancionSeleccionada = (Cancion)listaCanciones.SelectedItem;

            if (listaCanciones.SelectedItem != null)
            {
                EditarAlAire editarAlAire = new EditarAlAire(cancionSeleccionada);
                editarAlAire.Owner = this;
                editarAlAire.Show();
                this.Hide();
            }
        }
    }
}