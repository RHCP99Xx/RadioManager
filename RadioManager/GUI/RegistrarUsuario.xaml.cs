using System.Windows;
using System.Windows.Controls;
using RadioManager.Helpers.Validadores;
using RadioManager.Models.DAO;
using RadioManager.Models.POCO;
using RadioManager.Helpers.EncryptDataPackage;
using System.Windows.Media;
using System;
using MySql.Data.MySqlClient;
using System.ComponentModel;

namespace RadioManager.GUI
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuario.xaml
    /// </summary>
    public partial class RegistrarUsuario : Window
    {

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public RegistrarUsuario()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.windowTitle.Text = "Registrar Usuario";
            Loaded += ToolWindow_Loaded;
        }


        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;

            if (text.Equals("Creativo"))
            {
                this.personalNumberBox.Visibility = Visibility.Hidden;
                this.usernameBox.Visibility = Visibility.Visible;
            }
            else if (text.Equals("Operador de cabina"))
            {
                this.usernameBox.Visibility = Visibility.Hidden;
                this.personalNumberBox.Visibility = Visibility.Visible;
            }

        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            string name = this.nameBox.Text;
            string lastName = this.lastNameBox.Text;
            string email = this.emailBox.Text;
            string password = this.PasswordBox.Password;
            string confirmPassword = this.PasswordBox_Copy.Password;
            string userName = this.usernameBox.Text;
            string personalNumber = this.personalNumberBox.Text;



            if (Validator.EmailStructureValidator(email))
            {
                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(confirmPassword))
                {
                    if (password.Equals(confirmPassword))
                    {
                        if (this.userTypeCombo.Text.Equals(""))
                        {
                            MessageBox.Show("No se ha seleccionado un tipo de usuario", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            this.userTypeCombo.BorderBrush = Brushes.Red;

                        }
                        else if (userTypeCombo.Text.Equals("Operador de cabina"))
                        {
                            if (!string.IsNullOrEmpty(personalNumber))
                            {
                                Operador operador = new Operador
                                {
                                    Nombre = name,
                                    Apellidos = lastName,
                                    Correo = email,
                                    Contrasenia = EncryptData.Encrypt(password),
                                    NumPersonal = personalNumber
                                };

                                UsuarioDAO usuarioDAO = new UsuarioDAO();
                                bool existe = usuarioDAO.comprobarExistenciaOperador(operador.NumPersonal);
                                bool existeCorreo = usuarioDAO.existeEmail(operador.Correo);
                                bool registered = false;
                                if (existe)
                                {
                                    MessageBox.Show("El operador de cabina con este numero de personal ya se encuentra registrado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                else
                                {
                                    if (existeCorreo)
                                    {
                                        MessageBox.Show("El correo electrónico que ha introducido ya existe, intente con uno nuevo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                    else
                                    {
                                        registered = usuarioDAO.registrarUsuarioCabina(operador);
                                    }
                                }
                                if (registered)
                                {
                                    MessageBox.Show("El creativo se ha registrado satisfactoriamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("El número de personal se encuentra vacío", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                this.personalNumberBox.BorderBrush = Brushes.Red;
                                this.nameBox.BorderBrush = Brushes.Black;
                                this.lastNameBox.BorderBrush = Brushes.Black;
                                this.emailBox.BorderBrush = Brushes.Black;
                                this.PasswordBox.BorderBrush = Brushes.Black;
                                this.PasswordBox_Copy.BorderBrush = Brushes.Black;
                                this.usernameBox.BorderBrush = Brushes.Black;
                            }
                        }
                        else if (userTypeCombo.Text.Equals("Creativo"))
                        {
                            if (!string.IsNullOrEmpty(userName))
                            {
                                Creativo creativo = new Creativo
                                {
                                    Nombre = name,
                                    Apellidos = lastName,
                                    Correo = email,
                                    Contrasenia = EncryptData.Encrypt(password),
                                    Username = userName
                                };

                                UsuarioDAO usuarioDAO = new UsuarioDAO();
                                bool existe = usuarioDAO.comprobarExistenciaCreativo(creativo.Username);
                                bool existeCorreo = usuarioDAO.existeEmail(creativo.Correo);
                                bool registered = false;
                                if (existe)
                                {
                                    MessageBox.Show("El creativo con este nombre de usuario ya se encuentra registrado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                else
                                {
                                    if (existeCorreo)
                                    {
                                        MessageBox.Show("El correo electrónico que ha introducido ya existe, intente con uno nuevo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                    else
                                    {
                                        registered = usuarioDAO.registrarUsuarioCreativo(creativo);
                                    }
                                }
                                
                                if (registered)
                                {
                                    MessageBox.Show("El creativo se ha registrado satisfactoriamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("El campo de nombre de usuario se encuentra vacío", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                this.usernameBox.BorderBrush = Brushes.Red;
                                this.personalNumberBox.BorderBrush = Brushes.Black;
                                this.nameBox.BorderBrush = Brushes.Black;
                                this.lastNameBox.BorderBrush = Brushes.Black;
                                this.emailBox.BorderBrush = Brushes.Black;
                                this.PasswordBox.BorderBrush = Brushes.Black;
                                this.PasswordBox_Copy.BorderBrush = Brushes.Black;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinciden, asegurate de que sean iguales", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        this.PasswordBox.BorderBrush = Brushes.Red;
                        this.PasswordBox_Copy.BorderBrush = Brushes.Red;
                        this.personalNumberBox.BorderBrush = Brushes.Black;
                        this.nameBox.BorderBrush = Brushes.Black;
                        this.lastNameBox.BorderBrush = Brushes.Black;
                        this.emailBox.BorderBrush = Brushes.Black;
                        this.usernameBox.BorderBrush = Brushes.Black;
                    }
                }
                else
                {
                    MessageBox.Show("Algunos campos se encuentran vacíos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.nameBox.BorderBrush = Brushes.Red;
                    this.lastNameBox.BorderBrush = Brushes.Red;
                    this.PasswordBox.BorderBrush = Brushes.Red;
                    this.PasswordBox_Copy.BorderBrush = Brushes.Red;
                    this.emailBox.BorderBrush = Brushes.Black;
                    this.personalNumberBox.BorderBrush = Brushes.Black;
                    this.usernameBox.BorderBrush = Brushes.Black;

                }
            }
            else
            {
                MessageBox.Show("El correo electrónico no tiene una estructura correcta", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.emailBox.BorderBrush = Brushes.Red;
                this.personalNumberBox.BorderBrush = Brushes.Black;
                this.nameBox.BorderBrush = Brushes.Black;
                this.PasswordBox.BorderBrush = Brushes.Black;
                this.PasswordBox_Copy.BorderBrush = Brushes.Black;
                this.usernameBox.BorderBrush = Brushes.Black;
            }

        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ListaDeUsuarios listaDeUsuarios = new ListaDeUsuarios();
            listaDeUsuarios.Show();
            this.Close();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            
        }

    }
}