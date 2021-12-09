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
using RadioManager.Models.DAO;
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
            lbNombrePrograma.Content = programaSeleccionado.Nombre;
            if (programaSeleccionado.Activo == true)
            {
                lbEstado.Content = "Activo";
            }
            else
            {
                lbEstado.Content = "Inactivo";
            }
            lbRadio.Content = "Radio Local";

            CalendarioDAO calendarioDAO = new CalendarioDAO();
            Calendario calendario = new Calendario();

            calendario = calendarioDAO.getCalendarioPorIdPrograma(programaSeleccionado.IdPrograma);

            lbPatron.Content = calendario.IdPatron;
            lbComerciales.Content = calendario.CorteComercial;
            mostrarContenidoChecks(calendario.Dia);
            lbHrInicio.Content = calendario.HoraInicio;
            lbHoraFin.Content = calendario.HoraFin;
        }

        public void mostrarContenidoChecks(String dias)
        {
            string mostrarDias = "";
            if (dias.Contains("l"))
            {
                mostrarDias += "L";
            }
            if (dias.Contains("m"))
            {
                mostrarDias += ",M";
            }
            if (dias.Contains("x"))
            {
                mostrarDias += ",X";
            }
            if (dias.Contains("j"))
            {
                mostrarDias += ",J";
            }
            if (dias.Contains("v"))
            {
                mostrarDias += ",V";
            }
            if (dias.Contains("s"))
            {
                mostrarDias += ",S";
            }
            if (dias.Contains("d"))
            {
                mostrarDias += ",D";
            }

            lbTransmision.Content = mostrarDias;
        }

        public void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            EditarPrograma editarPrograma = new EditarPrograma(programaSeleccionado);
            editarPrograma.Owner = this;
            editarPrograma.Show();
            this.Hide();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MenuProgramas menuProgramas = new MenuProgramas();
            menuProgramas.Show();
            this.Close();
        }

        private void btnAsignar_Click(object sender, RoutedEventArgs e)
        {
            AsignarPatron asignarPatron = new AsignarPatron(programaSeleccionado);
            asignarPatron.Owner = this;
            asignarPatron.Show();
            this.Hide();
        }

        private void btnVerLista_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}