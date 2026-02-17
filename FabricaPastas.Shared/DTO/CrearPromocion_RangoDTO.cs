using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.Shared.DTO
{
    public class CrearPromocion_RangoDTO
    {
        [Required]
        public int Promocion_Id { get; set; }

        public int? Tipo_Cliente_Id { get; set; } // null = todos

        [Range(0, int.MaxValue)]
        public int MinPedidos { get; set; } = 0;

        public int? MaxPedidos { get; set; } = null;

        [Range(0, 100)]
        public decimal Descuento_Porcentaje { get; set; } = 0m;
    }
}
