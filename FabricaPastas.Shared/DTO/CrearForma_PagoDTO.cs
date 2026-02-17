using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearForma_PagoDTO
    {
        #region Atributos de Validación
        [Required(ErrorMessage = "El campo Método es obligatorio")]
        [MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Metodo { get; set; }
        #endregion
    }
}
