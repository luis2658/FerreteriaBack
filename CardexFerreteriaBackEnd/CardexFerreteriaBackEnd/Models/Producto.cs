using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardexFerreteriaBackEnd.Models
{
    public class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string categoria { get; set; }
        public double precio { get; set; }
        public int stock { get; set; }
    }
}
