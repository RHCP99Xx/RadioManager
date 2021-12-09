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
using RadioManager.Models.DAO;

namespace RadioManager.GUI
{
    /// <summary>
    /// Lógica de interacción para RegistrarPrograma.xaml
    /// </summary>
    public partial class FormularioPrograma : Window
    {
        public FormularioPrograma()
        {
            InitializeComponent();
        }

        public FormularioPrograma(Programa programa)
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            String diasTransmision = verificarChecks();
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(tpHrInicio.Text) || string.IsNullOrWhiteSpace(tpHrFin.Text) || string.IsNullOrWhiteSpace(tbCanciones.Text) || string.IsNullOrWhiteSpace(tbComerciales.Text))
            {
                this.txtNombre.BorderBrush = Brushes.Red;
                this.tpHrInicio.BorderBrush = Brushes.Red;
                this.tpHrFin.BorderBrush = Brushes.Red;
                this.tbCanciones.BorderBrush = Brushes.Red;
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

                    Programa programa = new Programa();
                    programa.Nombre = txtNombre.Text;
                    programa.Activo = true;
                    programa.IdRadio = 1;

                    bool programaRegistrado = programaDAO.registrarPrograma(programa);


                    CalendarioDAO calendarioDAO = new CalendarioDAO();

                    int idPrograma = programaDAO.obtenerIdPorNombre(txtNombre.Text);

                    Calendario calendario = new Calendario();
                    calendario.IdPrograma = idPrograma;
                    calendario.IdPatron = 1;
                    calendario.CorteComercial = tbComerciales.Text;
                    calendario.Dia = diasTransmision;
                    calendario.HoraInicio = tpHrInicio.Text;
                    calendario.HoraFin = tpHrFin.Text;

                    bool calendarioRegistrado = calendarioDAO.registrarCalendario(calendario);

                    if (programaRegistrado && calendarioRegistrado)
                    {
                        MessageBox.Show("Programa registrado");
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
            MenuProgramas menuProgramas = new MenuProgramas();
            menuProgramas.Show();
            this.Close();
        }
    }
}