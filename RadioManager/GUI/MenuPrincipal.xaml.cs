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
        }
        public MenuPrincipal(Creativo creativo)
        {
            this.creativo = creativo;
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
        }
        public MenuPrincipal(Operador operador)
        {
            this.operador = operador;
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
        }
        public MenuPrincipal(Usuario usuario)
        {
            this.usuario = usuario;
            InitializeComponent();
            this.windowTitle.Text = "Menú Principal";
        }
    }
}
