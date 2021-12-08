using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class PatronPrograma
    {
        private int idPatron;
        private int idPrograma;
        private string corteComercial;
        private string dia;
        private DateTime horaInicio;
        private DateTime horaFin;

        public int IdPatron { get => idPatron; set => idPatron = value; }
        public int IdPrograma { get => idPrograma; set => idPrograma = value; }
        public string CorteComercial { get => corteComercial; set => corteComercial = value; }
        public string Dia { get => dia; set => dia = value; }
        public DateTime HoraInicio { get => horaInicio; set => horaInicio = value;  }
        public DateTime HoraFin { get => horaFin; set => horaFin = value;  }
    }
}
