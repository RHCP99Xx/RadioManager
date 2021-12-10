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

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public MenuPrincipal()
        {
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Loaded += ToolWindow_Loaded;
        }
        public MenuPrincipal(Creativo creativo)
        {
            this.creativo = creativo;
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
            this.buttonUsuarios.Visibility = Visibility.Hidden;
            this.nameLabel.Text = creativo.Nombre;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.buttonUsuarios.Visibility = Visibility.Hidden;
            Loaded += ToolWindow_Loaded;
        }
        public MenuPrincipal(Operador operador)
        {
            this.operador = operador;
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
            this.nameLabel.Text = operador.Nombre;
            this.buttonUsuarios.Visibility = Visibility.Hidden;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            this.buttonProgramas.Visibility = Visibility.Hidden;
            this.buttonPatrones.Visibility = Visibility.Hidden;
            this.buttonCanciones.Visibility = Visibility.Hidden;
            this.buttonArtistas.Visibility = Visibility.Hidden;
            Loaded += ToolWindow_Loaded;
        }
        public MenuPrincipal(Usuario usuario)
        {
            this.usuario = usuario;
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
            this.nameLabel.Text = usuario.Nombre;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Loaded += ToolWindow_Loaded;
        }


        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void buttonPatrones_Click(object sender, RoutedEventArgs e)
        {
            Patrones patrones = new Patrones();
            patrones.ShowDialog();
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
            MenuReportes reportes = new MenuReportes();
            reportes.ShowDialog();
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
