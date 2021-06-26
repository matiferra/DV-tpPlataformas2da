using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Agencia
    {
        public int id { get; set; }
        public static int CantAloj { get; set; }
        public Hotel hotel { get; set; }
        public Cabania cabania { get; set; }
        public int cantAlojamientos { get; set; }

    }
}
