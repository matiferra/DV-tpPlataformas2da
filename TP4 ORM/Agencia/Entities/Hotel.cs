using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Hotel
    {
        public int id { get; set; }
        public string barrio { get; set; }
        public string estrellas { get; set; }
        public int cantidadDePersonas { get; set; }
        public bool tv { get; set; }
        public Ciudades ciudad { get; set; }
        public double precio_por_persona { get; set; }
        public bool reservado { get; set; }
    }
}
