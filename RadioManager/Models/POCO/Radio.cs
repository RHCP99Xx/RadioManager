using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class Radio
    {
        private int idRadio;
        private string nombre;

        public int IdRadio { get => idRadio; set => idRadio = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
