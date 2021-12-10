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
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        Creativo creativo;
        Operador operador;
        Usuario usuario;
        public MenuPrincipal()
        {
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        public MenuPrincipal(Creativo creativo)
        {
            this.creativo = creativo;
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
            this.buttonUsuarios.Visibility = Visibility.Hidden;
            this.nameLabel.Text = creativo.Nombre;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        public MenuPrincipal(Operador operador)
        {
            this.operador = operador;
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
            this.nameLabel.Text = operador.Nombre;
            this.buttonUsuarios.Visibility = Visibility.Hidden;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        public MenuPrincipal(Usuario usuario)
        {
            this.usuario = usuario;
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
            this.nameLabel.Text = usuario.Nombre;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void buttonPatrones_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonProgramas_Click(object sender, RoutedEventArgs e)
        {
            MenuProgramas programas = new MenuProgramas();
            programas.ShowDialog();
        }

        private void buttonArtistas_Click(object sender, RoutedEventArgs e)
        {
            ArtistMainWindow artistas = new ArtistMainWindow();
            artistas.ShowDialog();
        }

        private void buttonReportes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonCanciones_Click(object sender, RoutedEventArgs e)
        {
            IMenuCanciones canciones = new IMenuCanciones();
            canciones.ShowDialog();
        }

        private void buttonUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ListaDeUsuarios usuarios = new ListaDeUsuarios();
            usuarios.ShowDialog();
        }

        private void buttonSalir_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
