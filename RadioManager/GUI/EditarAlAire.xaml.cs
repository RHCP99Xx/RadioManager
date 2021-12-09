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
    /// Lógica de interacción para EditarAlAire.xaml
    /// </summary>
    public partial class EditarAlAire : Window
    {
        Cancion cancionSeleccionada = new Cancion();
        public EditarAlAire(Cancion cancion)
        {
            InitializeComponent();
            DataContext = this;
            cancionSeleccionada = cancion;

            lbNombrePrograma.Content = cancionSeleccionada.Titulo;
        }

        public void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            this.Close();
        }

        public void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string diasTransmision = verificarChecks();
            if (diasTransmision.Length > 0)
            {
                try
                {
                    CancionDAO cancionDAO = new CancionDAO();
                    cancionSeleccionada.Dias = diasTransmision;

                    bool resultado = cancionDAO.editarDias(cancionSeleccionada);

                    if (resultado)
                    {
                        MessageBox.Show("¡Canción editada exitosamente!");
                        Owner.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error y no se pudo editar la canción, inténtelo de nuevo");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error con la conexión a la base de datos", "ERROR");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private string verificarChecks()
        {
            string dias = "";

            if ((bool)cbLunes.IsChecked)
            {
                dias += "1";
            }
            if ((bool)cbMartes.IsChecked)
            {
                dias += "2";
            }
            if ((bool)cbMiercoles.IsChecked)
            {
                dias += "3";
            }
            if ((bool)cbJueves.IsChecked)
            {
                dias += "4";
            }
            if ((bool)cbViernes.IsChecked)
            {
                dias += "5";
            }
            if ((bool)cbSabado.IsChecked)
            {
                dias += "6";
            }
            if ((bool)cbDomingo.IsChecked)
            {
                dias += "7";
            }
            return dias;
        }
    }
}