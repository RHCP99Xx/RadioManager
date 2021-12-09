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
    /// Interaction logic for ReporteCancionesSinUsar.xaml
    /// </summary>
    public partial class ReporteCancionesSinUsar : Window
    {
        List<LineaPatronReporte> lineaPatronReportes;
        public ReporteCancionesSinUsar()
        {
            InitializeComponent();

            LineaPatronDAO lineaPatronDAO = new LineaPatronDAO();
            lineaPatronDAO.getLineasPatronSinUsar();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
