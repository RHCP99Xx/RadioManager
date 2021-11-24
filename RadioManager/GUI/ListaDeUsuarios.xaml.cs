using RadioManager.Models.DAO;
using System.Collections.ObjectModel;
using System.Windows;
using RadioManager.Models.POCO;
using System.Windows.Input;

namespace RadioManager.GUI
{
    
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

        private void Button_Agregar(object sender, RoutedEventArgs e)
        {

        }

        private void lvCreativos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var creativoSeleccionado = (TablaCreativos)lvCreativos.SelectedItem;
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            Creativo creativo = usuarioDAO.getCreativoById(creativoSeleccionado.IdCreativo);

            if (lvCreativos.SelectedItem != null)
            {
                DetallesDeUsuario detalle = new DetallesDeUsuario(creativo);
                detalle.Owner = this;
                detalle.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Escoja un artista", "ERROR");
            }

        }
    }
}
