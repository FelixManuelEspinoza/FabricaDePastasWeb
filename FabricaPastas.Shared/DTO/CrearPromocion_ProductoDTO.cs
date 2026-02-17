using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearPromocion_ProductoDTO
    {
        #region Atributos 
        [Required(ErrorMessage = "El Nombre del producto es obligatorio")]
        [MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Nombre_Producto { get; set; }

        [Required(ErrorMessage = "El campo Descuento_Porcentaje es obligatorio")]
        public decimal Descuento_Porcentaje { get; set; }

        [Required(ErrorMessage = "El campo Precio_Promocional es obligatorio")]
        public decimal Precio_Promocional { get; set; }
        #endregion
    }
}
