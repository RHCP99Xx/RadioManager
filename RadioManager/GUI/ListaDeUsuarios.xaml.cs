using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioManager.Models.DAO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using RadioManager.Models.POCO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace RadioManager.GUI
{
    /// <summary>
    /// Lógica de interacción para ListaDeUsuarios.xaml
    /// </summary>

    public partial class ListaDeUsuarios : Window
    {
        public ObservableCollection<TablaCreativos> Creativos { get; set; } = new ObservableCollection<TablaCreativos>();
        public ObservableCollection<TablaOperadores> Operadores { get; set; } = new ObservableCollection<TablaOperadores>();
        public struct TablaCreativos
        {
            public int IdCreativo { get; set; }
            public string NombreCompleto { get; set; }
            public string UserName { get; set; }
        }

        public struct TablaOperadores
        {
            public int IdOperador { get; set; }
            public string NombreCompleto { get; set; }
            public string NumeroPersonal { get; set; }
        }
        public ListaDeUsuarios()
        {
            InitializeComponent();
            this.windowTitle.Text = "Usuarios";
            DataContext = this;
            llenarTablaCreativosActivos();
            llenarTablaOperadoresActivos();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public void llenarTablaCreativosActivos()
        {
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            var creativos = usuarioDAO.getCreativosActivos();

            Creativos.Clear();

            lvCreativos.ItemsSource = Creativos;

            foreach (var creativo in creativos)
            {
                Creativos.Add(new TablaCreativos
                {
                    IdCreativo = creativo.IdUsuario,
                    NombreCompleto = creativo.Nombre + " " + creativo.Apellidos,
                    UserName = creativo.Username,

                });
            }
        }
        public void llenarTablaOperadoresActivos()
        {
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            var operadores = usuarioDAO.getOperadoresActivos();

            Operadores.Clear();

            lvOperadores.ItemsSource = Operadores;

            foreach (var operador in operadores)
            {
                Operadores.Add(new TablaOperadores
                {
                    IdOperador = operador.IdUsuario,
                    NombreCompleto = operador.Nombre + " " + operador.Apellidos,
                    NumeroPersonal = operador.NumPersonal,

                });
            }
        }

        private void lvCreativos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var creativoSeleccionado = (TablaCreativos)lvCreativos.SelectedItem;
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            Creativo creativo = usuarioDAO.getCreativoById(creativoSeleccionado.IdCreativo);

            if (lvCreativos.SelectedItem != null)
            {
                DetallesDeUsuario detallesUsuario = new DetallesDeUsuario(creativo);
                detallesUsuario.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Escoja un artista", "ERROR");
            }
        }

        private void lvOperadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var operadorSeleccionado = (TablaOperadores)lvOperadores.SelectedItem;
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            Operador operador = usuarioDAO.getOperadorById(operadorSeleccionado.IdOperador);

            if (lvOperadores.SelectedItem != null)
            {
                DetallesDeUsuario detallesUsuario = new DetallesDeUsuario(operador);
                detallesUsuario.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Escoja un artista", "ERROR");
            }
        }


        private void Button_Registrar(object sender, RoutedEventArgs e)
        {
            RegistrarUsuario registrarUsuario = new RegistrarUsuario();
            registrarUsuario.Owner = this;
            registrarUsuario.Show();
            this.Hide();
        }
    }
}