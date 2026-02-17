using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Lista_Precio_Id), nameof(Producto_Id), IsUnique = true)]
    public class Detalle_Lista_Precio : EntityBase
    {
        public int Lista_Precio_Id { get; set; }
        public Lista_Precio? Lista_Precio { get; set; }

        public int Producto_Id { get; set; }
        public Producto? Producto { get; set; }

        [Required]
        public decimal Precio { get; set; }
    }
}
