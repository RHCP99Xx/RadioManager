using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioManager.Models.POCO
{
    public class Artista
    {
        private int idArtista;
        private string nombreArtistico;
        private byte[] fotografia;
        private string descripcion;
        private bool activo;

        public int IdArtista { get => idArtista; set => idArtista = value; }
        public string NombreArtistico { get => nombreArtistico; set => nombreArtistico = value; }
        public byte[] Fotografia { get => fotografia; set => fotografia = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool Activo { get => activo; set => activo = value;  }
    }
}
