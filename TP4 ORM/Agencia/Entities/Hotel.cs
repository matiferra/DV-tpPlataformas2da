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
        public string nombre { get; set; }
        public string barrio { get; set; }
        public string estrellas { get; set; }
        public int cantidadDePersonas { get; set; }
        public bool tv { get; set; }
        public Ciudades ciudad { get; set; }
        public double precio_por_persona { get; set; }
        public bool reservado { get; set; }

        public Hotel() { }
        public Hotel(string nombre, string barrio, string estrellas, int cantidadDePersonas, bool tv, Ciudades ciudad
            ,double precio_por_persona, bool reservado)
        {
            this.barrio = barrio;
            this.estrellas = estrellas;
            this.cantidadDePersonas = cantidadDePersonas;
            this.tv = tv;
            this.ciudad = ciudad;
            this.precio_por_persona = precio_por_persona;
            this.reservado = reservado;
        }

    }
}
