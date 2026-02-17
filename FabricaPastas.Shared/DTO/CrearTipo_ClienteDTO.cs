using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearTipo_ClienteDTO
    {
        #region Atributos 
        [Required(ErrorMessage = "El campo Descripción es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Descripcion { get; set; }
        #endregion
    }
}
