using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class Patron
    {
        private int idPatron;
        private string nombre;
        private bool activo;

        public int IdPatron { get => idPatron; set => idPatron = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Activo { get => activo; set => activo = value;  }
    }
}
