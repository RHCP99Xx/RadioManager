using RadioManager.Models.DAO;
using RadioManager.Models.POCO;
using System;
using System.ComponentModel;
using System.Windows;

namespace RadioManager.GUI
{
    /// <summary>
    /// Lógica de interacción para DetallesDeUsuario.xaml
    /// </summary>
    public partial class DetallesDeUsuario : Window
    {

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        private Operador operador;
        private Creativo creativo;
        public DetallesDeUsuario(Creativo creativo)
        {
            this.creativo = creativo;
            InitializeComponent();
            this.windowTitle.Text = "Detalles del usuario";
            llenarDatosCreativo(creativo);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Loaded += ToolWindow_Loaded;
        }

        public DetallesDeUsuario(Operador operador)
        {
            this.operador = operador;
            InitializeComponent();
            this.windowTitle.Text = "Detalles del usuario";
            llenarDatosOperador(operador);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Loaded += ToolWindow_Loaded;
        }


        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        public void llenarDatosCreativo(Creativo creativo)
        {
            this.identifierText.Text = creativo.Username;
            this.nameLabel.Content = creativo.Nombre + " " + creativo.Apellidos;
            this.emailLabel.Content = creativo.Correo;
            this.userTypeLabel.Content = "Creativo";

            if (creativo.Activo == true)
            {
                this.statusLabel.Content = "Activo";
                this.activationButton.Content = "Desactivar";
            }
            else
            {
                this.statusLabel.Content = "Inactivo";
                this.activationButton.Content = "Activar";
            }
        }

        public void llenarDatosOperador(Operador operador)
        {
            this.identifierText.Text = operador.NumPersonal;
            this.nameLabel.Content = operador.Nombre + " " + operador.Apellidos;
            this.emailLabel.Content = operador.Correo;
            this.userTypeLabel.Content = "Creativo";

            if (operador.Activo == true)
            {
                this.statusLabel.Content = "Activo";
                this.activationButton.Content = "Desactivar";
            }
            else
            {
                this.statusLabel.Content = "Inactivo";
                this.activationButton.Content = "Activar";
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ListaDeUsuarios listaDeUsuarios = new ListaDeUsuarios();
            listaDeUsuarios.Show();
            this.Close();
        }

        private void editarButton_Click(object sender, RoutedEventArgs e)
        {
            if (creativo != null)
            {
                EditarUsuario editarUsuarioCreativo = new EditarUsuario(creativo);
                editarUsuarioCreativo.ShowDialog();
                //this.Close();
            }
            else if (operador != null)
            {
                EditarUsuario editarUsuarioOperador = new EditarUsuario(operador);
                editarUsuarioOperador.ShowDialog();
                //this.Close();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            
        }

        private void desactivarUsuario_Button(object sender, RoutedEventArgs e)
        {
            if (creativo != null)
            {
                if (activationButton.Content.Equals("Desactivar"))
                {
                    UsuarioDAO usuarioDAO = new UsuarioDAO();
                    usuarioDAO.desactivarCreativo(creativo.IdUsuario);
                    MessageBox.Show("El creativo ha sido desactivado satisfactoriamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.activationButton.Content = "Activar";
                    this.statusLabel.Content = "Inactivo";
                }
                else if (activationButton.Content.Equals("Activar"))
                {
                    UsuarioDAO usuarioDAO = new UsuarioDAO();
                    usuarioDAO.activarCreativo(creativo.IdUsuario);
                    MessageBox.Show("El creativo ha sido activado satisfactoriamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.activationButton.Content = "Desactivar";
                    this.statusLabel.Content = "Activo";
                }
            }
            else if(operador != null)
            {
                if (activationButton.Content.Equals("Desactivar"))
                {
                    UsuarioDAO usuarioDAO = new UsuarioDAO();
                    usuarioDAO.desactivarOperador(operador.IdUsuario);
                    MessageBox.Show("El operador de cabina ha sido desactivado satisfactoriamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.activationButton.Content = "Activar";
                    this.statusLabel.Content = "Inactivo";
                }
                else if (activationButton.Content.Equals("Activar"))
                {
                    UsuarioDAO usuarioDAO = new UsuarioDAO();
                    usuarioDAO.activarOperador(operador.IdUsuario);
                    MessageBox.Show("El operador de cabina ha sido activado satisfactoriamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.activationButton.Content = "Desactivar";
                    this.statusLabel.Content = "Activo";
                }
            }
        }
    }
}
