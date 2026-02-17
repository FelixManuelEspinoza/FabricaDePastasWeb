using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.Shared.DTO
{
    public class CrearPromocionDTO
    {
        [Required(ErrorMessage = "El campo Título es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Fecha_Inicio es obligatorio")]
        public DateOnly Fecha_Inicio { get; set; }

        [Required(ErrorMessage = "El campo Fecha_Fin es obligatorio")]
        public DateOnly Fecha_Fin { get; set; }

        [Required(ErrorMessage = "El campo Activo es obligatorio")]
        public bool Activa { get; set; }

        
        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100")]
        public decimal Descuento_Porcentaje { get; set; }
    }
}

