using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cabania
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string barrio { get; set; }
        public string estrellas { get; set; }
        public int cantidadDePersonas { get; set; }
        public bool tv { get; set; }
        public Ciudades ciudad { get; set; }
        public int cantidadDeBanios { get; set; }
        public double precio_por_dia { get; set; }
        public double habitaciones { get; set; }
        public bool reservado { get; set; }


        public Cabania()
        {

        }
        public Cabania(string nombre, string barrio, string estrellas, int cantidadDePersonas, bool tv, Ciudades ciudad,
            int cantidadDeBanios, double precio_por_dia, double habitaciones, bool reservado)
        {
            this.nombre = nombre;
            this.barrio = barrio;
            this.estrellas = estrellas;
            this.cantidadDePersonas = cantidadDePersonas;
            this.tv = tv;
            this.ciudad = ciudad;
            this.precio_por_dia = precio_por_dia;
            this.habitaciones = habitaciones;
            this.cantidadDeBanios = cantidadDeBanios;
            this.reservado = reservado;
        }
    }
}
