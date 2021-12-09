using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class Cancion
    {
        private int idCancion;
        private string titulo;
        private int idCategoria;
        private string nombreCategoria;
        private int idGenero;
        private string nombreGenero;
        private string clave;
        private string dias;
        private int idCantante;
        private string nombreArtista;
        private bool activo;
        private bool alAire;
        private byte[] imagen;

        public int IdCancion { get => idCancion; set => idCancion = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public int IdGenero { get => idGenero; set => idGenero = value; }
        public string Clave { get => clave; set => clave = value; }
        public string Dias { get => dias; set => dias = value; }
        public int IdCantante { get => idCantante; set => idCantante = value; }
        public bool Activo { get => activo; set => activo = value;  }
        public bool AlAire { get => alAire; set => alAire = value; }
        public byte[] Imagen { get => imagen; set => imagen = value; }
        public string NombreCategoria { get => nombreCategoria; set => nombreCategoria = value; }
        public string NombreGenero { get => nombreGenero; set => nombreGenero = value; }
        public string NombreArtista { get => nombreArtista; set => nombreArtista = value; }
    }
}
