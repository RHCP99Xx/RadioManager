using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class Programa
    {
        private int idPrograma;
        private string nombre;
        private bool activo;

        public int IdPrograma { get => idPrograma; set => idPrograma = value;  }
        public string Nombre { get => nombre; set => nombre = value;  }
        public bool Activo { get => activo; set => activo = value; }
    }
}
