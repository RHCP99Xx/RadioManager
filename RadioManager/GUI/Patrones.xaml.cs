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
    /// Interaction logic for Patrones.xaml
    /// </summary>
    public partial class Patrones : Window
    {
        ObservableCollection<Patron> patrones = new ObservableCollection<Patron>();
        Patron patronSeleccionado;
        public Patrones()
        {
            InitializeComponent();
            cargarPatrones();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public void cargarPatrones()
        {
            PatronDAO patronDAO = new PatronDAO();
            
            patrones = new ObservableCollection<Patron>(patronDAO.getTodoPatrones());

            lvPatrones.ItemsSource = patrones;
        }

        private void lvPatrones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvPatrones.SelectedItem != null)
            {
                patronSeleccionado = (Patron)lvPatrones.SelectedItem;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lvPatrones.SelectedItem != null)
            {
                MessageBoxResult resultado = MessageBox.Show("¿Está seguro de eliminar el patrón", "Confirmar acción", MessageBoxButton.OKCancel);
                if (resultado == MessageBoxResult.OK)
                {
                
                    PatronDAO patronDAO = new PatronDAO();
                    patronDAO.desactivarPatron(patronSeleccionado);
                    patrones.Remove(patronSeleccionado);

                    MessageBox.Show("Patrón eliminado");
                
                }
            }
            else
            {
                MessageBox.Show("Seleccione un patrón");
            }

        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarPatron agregarPatron = new AgregarPatron();
            agregarPatron.Show();
            this.Close();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

            if (lvPatrones.SelectedItem != null)
            {
                EditarPatron editarPatron = new EditarPatron(patronSeleccionado);
                editarPatron.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione un patrón");
            }
        }

    }
}
