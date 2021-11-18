using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class Cancion
    {
        private string clave;
        private string titulo;
        private string dias;
        private bool activo;
        private bool alAire;

        public string Clave { get => clave; set => clave = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Dias { get => dias; set => dias = value; }
        public bool Activo { get => activo; set => activo = value;  }
        public bool AlAire { get => alAire; set => alAire = value;  }
    }
}
