using MySql.Data.MySqlClient;
using RadioManager.Models.BD;
using System;
using System.Windows;


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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            try
            {
                MySqlConnection conn = null;
                conn = ConexionDB.getConnection();
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
