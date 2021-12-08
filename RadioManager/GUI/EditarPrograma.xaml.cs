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
            DetallesPrograma();
        }

        private void DetallesPrograma()
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
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(cbEstado.Text))
            {
                this.txtNombre.BorderBrush = Brushes.Red;
                this.cbEstado.BorderBrush = Brushes.Red;
                MessageBox.Show("Campos vacíos");
            }
            else
            {
                try
                {
                    ProgramaDAO programaDAO = new ProgramaDAO();

                    string nombre = txtNombre.Text;
                    string estado = cbEstado.Text;

                    Programa programa = new Programa();
                    programa.Nombre = nombre;
                    programa.Activo = true;
                    programa.IdRadio = 1;

                    bool programaEditado = programaDAO.editarPrograma(programa);

                    if (programaEditado)
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            this.Close();
        }
    }
}