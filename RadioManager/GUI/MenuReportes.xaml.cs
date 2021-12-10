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
    /// Interaction logic for MenuReportes.xaml
    /// </summary>
    public partial class MenuReportes : Window
    {
        public MenuReportes()
        {
            InitializeComponent();
        }

        private void btnReporteGeneral_Click(object sender, RoutedEventArgs e)
        {
            ReporteGeneralCanciones reporteGeneralCanciones = new ReporteGeneralCanciones();
            reporteGeneralCanciones.Show();
            this.Close();
        }

        private void btnReporteCancioneSinUsar_Click(object sender, RoutedEventArgs e)
        {
            ReporteCancionesSinUsar reporteCancionesSinUsar = new ReporteCancionesSinUsar();
            reporteCancionesSinUsar.Show();
            this.Close();
        }
    }
}
