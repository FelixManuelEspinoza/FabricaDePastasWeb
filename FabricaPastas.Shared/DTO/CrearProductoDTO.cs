using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearProductoDTO
    {
        #region Atributos
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo PrecioBase es obligatorio")]
        public decimal? PrecioBase { get; set; }

        [Required(ErrorMessage = "La imagen del producto es obligatoria")]
        public string? Imagen_Url { get; set; }

        [Required(ErrorMessage = "El campo Stock es obligatorio")]
        public int? Stock { get; set; }
        #endregion
    }
}
