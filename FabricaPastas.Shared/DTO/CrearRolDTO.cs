using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearRolDTO
    {

        #region Atributos 
        [Required(ErrorMessage = "El campo Nombre_Rol es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Nombre_rol { get; set; }
        #endregion
    }
}
