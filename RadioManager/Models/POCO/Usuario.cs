using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class Usuario
    {
        private int idUsuario;
        private string nombre;
        private string apellidos;
        private string correo;
        private string contrasenia;
        private bool activo;

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Contrasenia { get => contrasenia; set => contrasenia = value;  }
        public bool Activo { get => activo; set => activo = value;  }
    }
}
