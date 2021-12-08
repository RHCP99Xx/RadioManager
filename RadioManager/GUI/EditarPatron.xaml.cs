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
    /// Interaction logic for EditarPatron.xaml
    /// </summary>
    public partial class EditarPatron : Window
    {
        List<Genero> generos;
        List<Categoria> categorias;
        ObservableCollection<LineaPatron> lineaPatrones = new ObservableCollection<LineaPatron>();
        LineaPatron lineaPatronSeleccionada;
        Patron patron;

        public EditarPatron(Patron patron)
        {
            this.patron = patron;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            cargarCategorias();
            cargarGeneros();
            cargarLineasPatron();

            tbNombre.Text = patron.Nombre;
        }


        private void cargarGeneros()
        {
            GeneroDAO generoDAO = new GeneroDAO();
            generos = generoDAO.getGeneros();
            generos.RemoveAt(0);
            cbGenero.DisplayMemberPath = "NombreGenero";
            cbGenero.ItemsSource = generos;
        }

        private void cargarCategorias()
        {
            CategoriaDAO CategoriaDAO = new CategoriaDAO();
            categorias = CategoriaDAO.getCategorias();
            categorias.RemoveAt(0);
            cbCategoria.DisplayMemberPath = "NombreCategoria";
            cbCategoria.ItemsSource = categorias;
        }

        private void cargarLineasPatron()
        {
            LineaPatronDAO lineaPatronDAO = new LineaPatronDAO();
            lineaPatrones = new ObservableCollection<LineaPatron>(lineaPatronDAO.getLineasPatron(patron));
            lvLineasPatron.ItemsSource = lineaPatrones;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if ((cbCategoria.SelectedItem != null) && (cbGenero.SelectedItem != null))
            {
                LineaPatron lineaPatron = new LineaPatron();
                lineaPatron.Categoria = (Categoria)cbCategoria.SelectedItem;
                lineaPatron.Genero = (Genero)cbGenero.SelectedItem;
                lineaPatrones.Add(lineaPatron);
            }
            else
            {
                MessageBox.Show("Por favor seleccione un genéro y una categoría");
            }


        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (tbNombre.Text == "")
            {
                MessageBox.Show("Por favor ingrese un nombre");
            }
            else if (lineaPatrones.Count == 0)
            {
                MessageBox.Show("Por favor ingrese una linea la patrón");
            }
            else
            {
                patron.Nombre = tbNombre.Text;
             
                PatronDAO patronDAO = new PatronDAO();
                patronDAO.actualizarNombrePatron(patron);


                LineaPatronDAO lineaPatronDAO = new LineaPatronDAO();

                lineaPatronDAO.eliminarLineaPatron(patron);

                int posicion = 1;
                foreach (LineaPatron lineaPatronActual in lineaPatrones)
                {
                    lineaPatronActual.Posicion = posicion;
                    lineaPatronActual.IdPatron = patron.IdPatron;
                    
                    lineaPatronDAO.guardarLineaPatron(lineaPatronActual);
                    posicion++;
                }


                MessageBox.Show("Patrón modificado con exito");

                Patrones patrones = new Patrones();
                patrones.Show();
                this.Close();
            }


        }

        private void btnEliminarLinea_Click(object sender, RoutedEventArgs e)
        {
            if (lvLineasPatron.SelectedItem != null)
            {
                lineaPatrones.Remove(lineaPatronSeleccionada);
            }
            else
            {
                MessageBox.Show("Seleccione un linea del patrón");
            }
        }

        private void lvLineasPatron_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvLineasPatron.SelectedItem != null)
            {
                lineaPatronSeleccionada = (LineaPatron)lvLineasPatron.SelectedItem;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Patrones patrones = new Patrones();
            patrones.Show();
            this.Close();
        }
    }
}
