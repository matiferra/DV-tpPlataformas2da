using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AgenciaManager
    {
        public int id { get; set; }
        public Usuario usuario { get; set; }
        public Reserva reserva { get; set; }
        public Agencia agencia { get; set; }
    }
}
