using RadioManager.Models.POCO;
using System.Windows;
using RadioManager.Helpers.Validadores;
using RadioManager.Helpers.EncryptDataPackage;
using RadioManager.Models.DAO;
using System.Windows.Media;
using System.ComponentModel;

namespace RadioManager.GUI
{
    /// <summary>
    /// Lógica de interacción para EditarUsuario.xaml
    /// </summary>
    public partial class EditarUsuario : Window
    {
        private Creativo creativoSeleccionado = new Creativo();
        private Operador operadorSeleccionado = new Operador();
        public EditarUsuario(Creativo creativo)
        {
            this.creativoSeleccionado = creativo;
            InitializeComponent();
            llenarCamposCreativo(creativo);
            this.windowTitle.Text = "Editar Creativo";
            this.confirmButton.Click += confirmarCambiosCreativo_Button;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public EditarUsuario(Operador operador)
        {
            this.operadorSeleccionado = operador;
            InitializeComponent();
            llenarCamposOperador(operador);
            this.windowTitle.Text = "Editar Operador";
            this.confirmButton.Click += confirmarCambiosOperador_Button;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }


        public void llenarCamposCreativo(Creativo creativo)
        {
            this.nameBox.Text = creativo.Nombre;
            this.lastNameBox.Text = creativo.Apellidos;
            this.emailBox.Text = creativo.Correo;
            this.password.Text = EncryptData.Decrypt(creativo.Contrasenia);
            this.usernameBox.Visibility = Visibility.Visible;
            this.usernameBox.Text = creativo.Username;
        }

        public void llenarCamposOperador(Operador operador)
        {
            this.nameBox.Text = operador.Nombre;
            this.lastNameBox.Text = operador.Apellidos;
            this.emailBox.Text = operador.Correo;
            this.password.Text = EncryptData.Decrypt(operador.Contrasenia);
            this.personalNumberBox.Visibility = Visibility.Visible;
            this.personalNumberBox.Text = operador.NumPersonal;
        }


        private void confirmarCambiosCreativo_Button(object sender, RoutedEventArgs e)
        {
            string nombre = this.nameBox.Text;
            string apellido = this.lastNameBox.Text;
            string correo = this.emailBox.Text;
            string nombreDeUsuario = this.usernameBox.Text;
            string password = this.password.Text;
            if (Validator.EmailStructureValidator(correo))
            {
                if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(apellido) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(nombreDeUsuario))
                {
                    if (Validator.checkPasswordLength(password))
                    {
                        creativoSeleccionado.Nombre = nombre;
                        creativoSeleccionado.Apellidos = apellido;
                        creativoSeleccionado.Correo = correo;
                        creativoSeleccionado.Username = nombreDeUsuario;
                        creativoSeleccionado.Contrasenia = EncryptData.Encrypt(password);

                        UsuarioDAO usuarioDAO = new UsuarioDAO();
                        bool creativoEditado = usuarioDAO.editarCreativo(creativoSeleccionado);
                        if (creativoEditado)
                        {
                            MessageBox.Show("El creativo se ha editado satisfactoriamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error al modificar, inténtelo más tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("La contraseña debe tener al menos 8 caracteres", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        this.password.BorderBrush = Brushes.Red;
                        this.nameBox.BorderBrush = Brushes.Black;
                        this.lastNameBox.BorderBrush = Brushes.Black;
                        this.emailBox.BorderBrush = Brushes.Black;
                        this.personalNumberBox.BorderBrush = Brushes.Black;

                    }
                }
                else
                {
                    MessageBox.Show("Algunos campos están vacíos, inténtalo nuevamente", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.password.BorderBrush = Brushes.Red;
                    this.nameBox.BorderBrush = Brushes.Red;
                    this.lastNameBox.BorderBrush = Brushes.Red;
                    this.emailBox.BorderBrush = Brushes.Black;
                    this.personalNumberBox.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                MessageBox.Show("El correo electrónico no tiene una estructura válida", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.password.BorderBrush = Brushes.Black;
                this.nameBox.BorderBrush = Brushes.Black;
                this.lastNameBox.BorderBrush = Brushes.Black;
                this.emailBox.BorderBrush = Brushes.Red;
                this.personalNumberBox.BorderBrush = Brushes.Black;
            }
        }

        private void confirmarCambiosOperador_Button(object sender, RoutedEventArgs e)
        {
            string nombre = this.nameBox.Text;
            string apellido = this.lastNameBox.Text;
            string correo = this.emailBox.Text;
            string numeroPersonal = this.personalNumberBox.Text;
            string password = this.password.Text;
            if (Validator.EmailStructureValidator(correo))
            {
                if (!string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(apellido) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(numeroPersonal))
                {
                    if (Validator.checkPasswordLength(password))
                    {
                        operadorSeleccionado.Nombre = nombre;
                        operadorSeleccionado.Apellidos = apellido;
                        operadorSeleccionado.Correo = correo;
                        operadorSeleccionado.NumPersonal = numeroPersonal;
                        operadorSeleccionado.Contrasenia = EncryptData.Encrypt(password);

                        UsuarioDAO usuarioDAO = new UsuarioDAO();
                        bool operadorEditado = usuarioDAO.editarOperador(operadorSeleccionado);
                        if (operadorEditado)
                        {
                            MessageBox.Show("El operador de cabina se ha editado satisfactoriamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error al modificar, inténtelo más tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("La contraseña debe tener al menos 8 caracteres", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        this.password.BorderBrush = Brushes.Red;
                        this.nameBox.BorderBrush = Brushes.Black;
                        this.lastNameBox.BorderBrush = Brushes.Black;
                        this.emailBox.BorderBrush = Brushes.Black;
                        this.personalNumberBox.BorderBrush = Brushes.Black;

                    }
                }
                else
                {
                    MessageBox.Show("Algunos campos están vacíos, inténtalo nuevamente", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.password.BorderBrush = Brushes.Red;
                    this.nameBox.BorderBrush = Brushes.Red;
                    this.lastNameBox.BorderBrush = Brushes.Red;
                    this.emailBox.BorderBrush = Brushes.Black;
                    this.personalNumberBox.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                MessageBox.Show("El correo electrónico no tiene una estructura válida", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.password.BorderBrush = Brushes.Black;
                this.nameBox.BorderBrush = Brushes.Black;
                this.lastNameBox.BorderBrush = Brushes.Black;
                this.emailBox.BorderBrush = Brushes.Red;
                this.personalNumberBox.BorderBrush = Brushes.Black;
            }
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
        }
    }
}
