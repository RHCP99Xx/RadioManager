using MySql.Data.MySqlClient;
using RadioManager.Models.BD;
using RadioManager.Models.DAO;
using RadioManager.Models.POCO;
using System;
using System.Windows;
using RadioManager.Helpers.Validadores;
using System.Windows.Media;

namespace RadioManager.GUI
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = this.emailBox.Text;
            string password = this.passwordBox.Password;

            UsuarioDAO usuario = new UsuarioDAO();

            if (Validator.EmailStructureValidator(email))
            {
                if (!string.IsNullOrEmpty(password))
                {

                    bool isLogged = usuario.login(email, password);
                    if (isLogged)
                    {
                        Creativo creativoPOCO = usuario.esCreativoLogin(email);

                        if (creativoPOCO.Nombre != null)
                        {
                            if (creativoPOCO.Activo)
                            {
                                ArtistMainWindow mainArtist = new ArtistMainWindow();
                                this.Hide();
                                mainArtist.Show();
                            }
                            else
                            {
                                MessageBox.Show("El usuario no se encuentra activo, por favor contacte al administrador del sistema", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            Operador operadorPOCO = usuario.esOperadorLogin(email);
                            if (operadorPOCO.Nombre != null)
                            {
                                if (operadorPOCO.Activo)
                                {
                                    IMenuCanciones menuCanciones = new IMenuCanciones();
                                    this.Hide();
                                    menuCanciones.Show();
                                }
                                else
                                {
                                    MessageBox.Show("El usuario no se encuentra activo, por favor contacte al administrador del sistema", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                            else
                            {
                                Usuario usuarioPOCO = usuario.esAdminLogin(email);
                                if (usuarioPOCO.Nombre != null)
                                {
                                    ListaDeUsuarios listaUsuarios = new ListaDeUsuarios();
                                    this.Hide();
                                    listaUsuarios.Show();
                                }
                                else
                                {
                                    MessageBox.Show("No se ha encontrado ningún usuario con las credenciales proporcionadas.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Algunos campos se encuentran vacíos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        this.emailBox.BorderBrush = Brushes.Black;
                        this.passwordBox.BorderBrush = Brushes.Red;
                    }
                }
                else
                {
                    MessageBox.Show("El correo electrónico no tiene una estructura correcta", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.emailBox.BorderBrush = Brushes.Red;
                    this.passwordBox.BorderBrush = Brushes.Black;
                }
            }
        }
    }
}
