using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.BD.Data.Entity
{
    public class Promocion : EntityBase
    {
        [Required, MaxLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        public DateOnly Fecha_Inicio { get; set; }
        public DateOnly Fecha_Fin { get; set; }

        public bool Activa { get; set; } = true;

        //  Descuento
        [Range(0, 100)]
        public decimal Descuento_Porcentaje { get; set; } = 0;

        // Navegación a productos asociados
        public ICollection<Promocion_Producto>? Productos { get; set; }


        public ICollection<Promocion_Rango>? Rangos { get; set; }
    }
}
