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
        private Categoria categoria;
        private Genero genero;
        private int idPatron;

        public int IdLineaPatron { get => idLineaPatron; set => idLineaPatron = value;  }
        public int Posicion { get => posicion; set => posicion = value; }
        public Categoria Categoria { get => categoria; set => categoria = value; }
        public Genero Genero { get => genero; set => genero = value; }
        public int IdPatron { get => idPatron; set => idPatron = value; }
    }
}
