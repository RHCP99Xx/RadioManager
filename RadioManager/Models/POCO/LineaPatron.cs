using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class LineaPatron
    {
        private int idLineaPatron;
        private int posicion;

        public int IdLineaPatron { get => idLineaPatron; set => idLineaPatron = value;  }
        public int Posicion { get => posicion; set => posicion = value; }
    }
}
