using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    class LineaPatronReporte : LineaPatron
    {
        private int numCanciones;
        public int NumCanciones { get => numCanciones; set => numCanciones = value; }
    }
}
