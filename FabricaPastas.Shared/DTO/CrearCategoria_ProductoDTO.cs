using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearCategoria_ProductoDTO
    {
        [Required(ErrorMessage = "El tipo de pasta es obligatorio")]
        [MaxLength(100)]
        public string Tipo_Pasta { get; set; } = string.Empty;

        [Required(ErrorMessage = "La forma es obligatoria")]
        [MaxLength(100)]
        public string Forma { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tamaño es obligatorio")]
        [MaxLength(100)]
        public string Tamanio { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? IngredientesBase { get; set; }

        [Required(ErrorMessage = "El proceso de elaboración es obligatorio")]
        [MaxLength(100)]
        public string ProcesoElaboracion { get; set; } = string.Empty;
    }
}
