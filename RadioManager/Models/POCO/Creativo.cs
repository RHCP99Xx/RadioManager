using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class Creativo : Usuario
    {
        private string username;

        public string Username { get => username; set => username = value;  }
    }
}
