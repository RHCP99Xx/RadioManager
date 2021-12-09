using RadioManager.Models.DAO;
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
    /// Lógica de interacción para AsignarPatron.xaml
    /// </summary>
    public partial class AsignarPatron : Window
    {
        Programa programaSelecionado = new Programa();
        public AsignarPatron(Programa programa)
        {
            InitializeComponent();
            DataContext = this;
            programaSelecionado = programa;
            mostrarDetallesPrograma();
        }

        public void mostrarDetallesPrograma()
        {
            lbNombrePrograma.Content = programaSelecionado.Nombre;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPatron.Text))
            {
                this.tbPatron.BorderBrush = Brushes.Red;
                MessageBox.Show("Ingrese un patrón");
            }
            else
            {
                try
                {
                    ProgramaDAO programaDAO = new ProgramaDAO();
                    CalendarioDAO calendarioDAO = new CalendarioDAO();

                    int idPrograma = programaDAO.obtenerIdPorNombre(programaSelecionado.Nombre);

                    Calendario calendario = new Calendario();
                    calendario.IdPrograma = idPrograma;
                    calendario.IdPatron = Int16.Parse(tbPatron.Text);

                    bool asignarPatron = calendarioDAO.asignarPatron(calendario);

                    bool activarPrograma = programaDAO.activarConPatron(programaSelecionado);

                    if (asignarPatron && activarPrograma)
                    {
                        MessageBox.Show("Patron asignado correctamente");
                        Owner.Owner.Show();
                        Owner.Close();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error y no se pudo modificar el artista, inténtelo de nuevo");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error", "Error");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            this.Close();
        }
    }
}