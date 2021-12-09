using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    class Calendario
    {
        private int idPatron;
        private int idPrograma;
        private String corteComercial;
        private String dia;
        private String horaInicio;
        private String horaFin;

        public int IdPatron { get => idPatron; set => idPatron = value; }
        public int IdPrograma { get => idPrograma; set => idPrograma = value; }
        public string CorteComercial { get => corteComercial; set => corteComercial = value; }
        public string Dia { get => dia; set => dia = value; }
        public String HoraInicio { get => horaInicio; set => horaInicio = value; }
        public String HoraFin { get => horaFin; set => horaFin = value; }
    }
}
