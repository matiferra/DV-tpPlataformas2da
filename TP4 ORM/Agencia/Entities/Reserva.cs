using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;




namespace Entities
{
    public class Reserva
    {
        public int id { get; set; }
        public DateTime FDesde { get; set; }
        public DateTime FHasta { get; set; }
        public Usuario usuario { get; set; }
        public float precio { get; set; }
        public Cabania cabania { get; set; }
        public Hotel hotel { get; set; }


    }
}

