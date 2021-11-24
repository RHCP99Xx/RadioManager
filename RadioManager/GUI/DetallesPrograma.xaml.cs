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
using RadioManager.Models.POCO;

namespace RadioManager.GUI
{
    /// <summary>
    /// Lógica de interacción para DetallesPrograma.xaml
    /// </summary>
    public partial class DetallesPrograma : Window
    {
        Programa programaSeleccionado = new Programa();
        public DetallesPrograma(Programa programa)
        {
            InitializeComponent();
            DataContext = this;
            programaSeleccionado = programa;
            mostrarDetalles();
        }

        private void mostrarDetalles()
        {
            nombrePrograma.Text = programaSeleccionado.Nombre;

            if (programaSeleccionado.Activo == true)
            {
                estado.Text = "Activo";
            }
            else
            {
                estado.Text = "Inactivo";
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            EditarPrograma editarPrograma = new EditarPrograma(programaSeleccionado);
            editarPrograma.Show();
            this.Close();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MenuProgramas menuProgramas = new MenuProgramas();
            menuProgramas.Show();
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MenuProgramas menuProgramas = new MenuProgramas();
            menuProgramas.Show();
            this.Close();
        }
    }
}