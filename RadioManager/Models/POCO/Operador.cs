using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class Operador : Usuario
    {
        private string numPersonal;

        public string NumPersonal { get => numPersonal; set => numPersonal = value; }
    }
}
