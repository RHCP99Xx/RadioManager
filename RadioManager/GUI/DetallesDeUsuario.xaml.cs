using System.Windows;
using RadioManager.Models.POCO;

namespace RadioManager.GUI
{
    /// <summary>
    /// Lógica de interacción para DetallesDeUsuario.xaml
    /// </summary>
    public partial class DetallesDeUsuario : Window
    {
        Creativo creativo;
        public DetallesDeUsuario(Creativo creativo)
        {
            this.creativo = creativo;
            InitializeComponent();
            this.windowTitle.Text = "Detalles del usuario";
            mostrarDetalles(creativo);
        }

        public void mostrarDetalles(Creativo creativo)
        {
            this.identifierText.Text = creativo.Username;
            this.nameLabel.Content = creativo.Nombre + " " + creativo.Apellidos;
            this.emailLabel.Content = creativo.Correo;
            this.userTypeLabel.Content = "Creativo";

            if (creativo.Activo == true)
            {
                this.userTypeLabel.Content = "Activo";
            }
        }
    }
}
