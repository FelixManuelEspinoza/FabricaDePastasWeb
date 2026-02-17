using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearDetalle_Lista_PrecioDTO
    {
        public int Lista_Precio_Id { get; set; }
        public int Producto_Id { get; set; }
        public decimal Precio { get; set; }
    }
}
