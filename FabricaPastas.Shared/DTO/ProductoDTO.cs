using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace FabricaPastas.Shared.DTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal PrecioBase { get; set; }
        public string? Imagen_Url { get; set; }
        public int Stock { get; set; }
    }
}
