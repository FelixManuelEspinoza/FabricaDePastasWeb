using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Promocion_Id), nameof(Producto_Id), IsUnique = true)]
    public class Promocion_Producto : EntityBase
    {
        public int Promocion_Id { get; set; }
        public Promocion? Promocion { get; set; }

        public int Producto_Id { get; set; }
        public Producto? Producto { get; set; }
    }
}
