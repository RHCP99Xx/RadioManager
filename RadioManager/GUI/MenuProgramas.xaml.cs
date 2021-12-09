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
    /// Lógica de interacción para Programas.xaml
    /// </summary>
    public partial class MenuProgramas : Window
    {
        public ObservableCollection<TablaProgramas> Programas { get; set; } = new ObservableCollection<TablaProgramas>();
        public MenuProgramas()
        {
            InitializeComponent();
            DataContext = this;
            mostrarProgramas();
        }

        public struct TablaProgramas
        {
            public int IdPrograma { get; set; }
            public String Nombre { get; set; }
        }

        public void mostrarProgramas()
        {
            ProgramaDAO programaDAO = new ProgramaDAO();
            var programas = programaDAO.getProgramas();

            Programas.Clear();

            foreach (var programa in programas)
            {
                Programas.Add(new TablaProgramas
                {
                    IdPrograma = programa.IdPrograma,
                    Nombre = programa.Nombre,
                });
            }
        }

        private void lvProgramas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var programaSeleccionado = (TablaProgramas)lvProgramas.SelectedItem;
            ProgramaDAO programaDAO = new ProgramaDAO();
            Programa programa = programaDAO.getProgramaPorId(programaSeleccionado.IdPrograma);

            if (lvProgramas.SelectedItem != null)
            {
                DetallesPrograma detallesPrograma = new DetallesPrograma(programa);
                detallesPrograma.Owner = this;
                detallesPrograma.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Escoja un programa", "ERROR");
            }
        }

        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                Programas.Clear();
                mostrarProgramas();
            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
                {
                    ProgramaDAO programaDAO = new ProgramaDAO();
                    var programa = programaDAO.getProgramaPorNombre(txtBusqueda.Text);

                    if (!programa.Equals(null))
                    {
                        Programas.Clear();
                        Programas.Add(new TablaProgramas
                        {
                            IdPrograma = programa.IdPrograma,
                            Nombre = programa.Nombre,
                        });
                    }
                    else
                    {
                        Programas.Clear();
                        MessageBox.Show("No existe ningún programa con ese nombre");
                        mostrarProgramas();
                    }
                }
                else
                {
                    Programas.Clear();
                    MessageBox.Show("Escriba el nombre de un programa");
                    mostrarProgramas();
                }
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            FormularioPrograma formularioPrograma = new FormularioPrograma();
            formularioPrograma.Show();
            this.Close();
        }
    }
}