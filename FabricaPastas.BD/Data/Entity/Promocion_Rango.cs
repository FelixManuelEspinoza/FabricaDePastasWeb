using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Promocion_Id), nameof(Tipo_Cliente_Id), nameof(MinPedidos), nameof(MaxPedidos))]
    public class Promocion_Rango : EntityBase
    {
        // FK
        public int Promocion_Id { get; set; }
        public Promocion? Promocion { get; set; }

        // opcional: si es null aplica a todos los tipos
        public int? Tipo_Cliente_Id { get; set; }
        public Tipo_Cliente? Tipo_Cliente { get; set; }

        // rango de pedidos
        [Range(0, int.MaxValue)]
        public int MinPedidos { get; set; } = 0;

        public int? MaxPedidos { get; set; } = null;

        // descuento
        [Range(0, 100)]
        public decimal Descuento_Porcentaje { get; set; } = 0m;
    }
}
