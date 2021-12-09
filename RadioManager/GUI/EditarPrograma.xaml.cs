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
    /// Lógica de interacción para EditarPrograma.xaml
    /// </summary>
    public partial class EditarPrograma : Window
    {
        Programa programaSeleccionado = new Programa();
        public EditarPrograma(Programa programa)
        {
            InitializeComponent();
            DataContext = this;
            programaSeleccionado = programa;
            llenarCombobox();
            detallesPrograma();
        }

        private void detallesPrograma()
        {
            txtNombre.Text = programaSeleccionado.Nombre;
            if (programaSeleccionado.Activo == true)
            {
                cbEstado.Text = "Activo";
            }
            else
            {
                cbEstado.Text = "Inactivo";
            }


            CalendarioDAO calendarioDAO = new CalendarioDAO();
            Calendario calendario = new Calendario();

            calendario = calendarioDAO.getCalendarioPorIdPrograma(programaSeleccionado.IdPrograma);

            tbPatron.Text = calendario.IdPatron.ToString();
            tbComerciales.Text = calendario.CorteComercial;

            String diasTransmision = calendario.Dia;
            llenarChecks(diasTransmision);

            tpHrInicio.Text = calendario.HoraInicio;
            tpHrFin.Text = calendario.HoraFin;
        }

        public void llenarChecks(String dias)
        {
            if (dias.Contains("l"))
            {
                cbLunes.IsChecked = true;
            }
            if (dias.Contains("m"))
            {
                cbMartes.IsChecked = true;
            }
            if (dias.Contains("x"))
            {
                cbMiercoles.IsChecked = true;
            }
            if (dias.Contains("j"))
            {
                cbJueves.IsChecked = true;
            }
            if (dias.Contains("v"))
            {
                cbViernes.IsChecked = true;
            }
            if (dias.Contains("s"))
            {
                cbSabado.IsChecked = true;
            }
            if (dias.Contains("d"))
            {
                cbDomingo.IsChecked = true;
            }
        }

        private void llenarCombobox()
        {
            List<string> listaFiltros = new List<string>();
            listaFiltros.Add("Activo");
            listaFiltros.Add("Inactivo");

            cbEstado.ItemsSource = listaFiltros;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            String diasTransmision = verificarChecks();
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(cbEstado.Text) || string.IsNullOrWhiteSpace(tpHrInicio.Text) || string.IsNullOrWhiteSpace(tpHrFin.Text) || string.IsNullOrWhiteSpace(tbPatron.Text) || string.IsNullOrWhiteSpace(tbComerciales.Text))
            {
                this.txtNombre.BorderBrush = Brushes.Red;
                this.cbEstado.BorderBrush = Brushes.Red;
                this.tpHrInicio.BorderBrush = Brushes.Red;
                this.tpHrFin.BorderBrush = Brushes.Red;
                this.tbPatron.BorderBrush = Brushes.Red;
                this.tbComerciales.BorderBrush = Brushes.Red;
                MessageBox.Show("Campos vacíos");
            }
            else if (diasTransmision.Length == 0)
            {
                this.cbLunes.BorderBrush = Brushes.Red;
                this.cbMartes.BorderBrush = Brushes.Red;
                this.cbMiercoles.BorderBrush = Brushes.Red;
                this.cbJueves.BorderBrush = Brushes.Red;
                this.cbViernes.BorderBrush = Brushes.Red;
                this.cbSabado.BorderBrush = Brushes.Red;
                this.cbDomingo.BorderBrush = Brushes.Red;
                MessageBox.Show("Los programas deben tener al menos 1 día de transmisión");
            }
            else
            {
                try
                {
                    ProgramaDAO programaDAO = new ProgramaDAO();

                    programaSeleccionado.Nombre = txtNombre.Text;

                    if (cbEstado.SelectedIndex == 0)
                    {
                        programaSeleccionado.Activo = true;
                    }
                    else
                    {
                        programaSeleccionado.Activo = false;
                    }

                    programaSeleccionado.IdRadio = 1;

                    bool programaEditado = programaDAO.editarPrograma(programaSeleccionado);

                    CalendarioDAO calendarioDAO = new CalendarioDAO();

                    int idPrograma = programaDAO.obtenerIdPorNombre(txtNombre.Text);

                    Calendario calendario = new Calendario();
                    calendario.IdPrograma = idPrograma;
                    calendario.IdPatron = Int16.Parse(tbPatron.Text);
                    calendario.CorteComercial = tbComerciales.Text;
                    calendario.Dia = diasTransmision;
                    calendario.HoraInicio = tpHrInicio.Text;
                    calendario.HoraFin = tpHrFin.Text;

                    Console.WriteLine(calendario.IdPatron);
                    Console.WriteLine(calendario.IdPrograma);
                    Console.WriteLine(calendario.CorteComercial);
                    Console.WriteLine(calendario.Dia);
                    Console.WriteLine(calendario.HoraInicio);
                    Console.WriteLine(calendario.HoraFin);

                    bool calendarioEditado = calendarioDAO.editarCalendario(calendario);

                    if (programaEditado && calendarioEditado)
                    {
                        MessageBox.Show("Programa editado");
                        MenuProgramas menuProgramas = new MenuProgramas();
                        menuProgramas.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private string verificarChecks()
        {
            string dias = "";

            if ((bool)cbLunes.IsChecked)
            {
                dias += "l";
            }
            if ((bool)cbMartes.IsChecked)
            {
                dias += "m";
            }
            if ((bool)cbMiercoles.IsChecked)
            {
                dias += "x";
            }
            if ((bool)cbJueves.IsChecked)
            {
                dias += "j";
            }
            if ((bool)cbViernes.IsChecked)
            {
                dias += "v";
            }
            if ((bool)cbSabado.IsChecked)
            {
                dias += "s";
            }
            if ((bool)cbDomingo.IsChecked)
            {
                dias += "d";
            }
            return dias;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            this.Close();
        }
    }
}