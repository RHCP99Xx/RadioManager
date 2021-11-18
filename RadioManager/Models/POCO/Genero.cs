using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class Genero
    {
        private int idGenero;
        private string nombreGenero;

        public int IdGenero { get => idGenero; set => idGenero = value;  }
        public string NombreGenero { get => nombreGenero; set => nombreGenero = value;  }
    }
}
