using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearUsuarioDTO
    {
        #region Atributos de Validación 
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo Nombre de usuario es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? NombreUsuario { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio")]
        [MaxLength(150, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El campo E-mail es obligatorio")]
        [MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        [MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Contraseña { get; set; }

        [Required(ErrorMessage = "El campo Teléfono es obligatorio")]
        [MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Teléfono { get; set; }

        [Required(ErrorMessage = "El campo Dirección es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Dirección { get; set; }

        [Required(ErrorMessage = "El campo Cuit_Cuil es obligatorio")]
        //[MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Cuit_Cuil { get; set; }

        //[Required(ErrorMessage = "El campo Fecha es obligatorio")]
        //public DateOnly Fecha_Registro { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el tipo de cliente")]
        public string TipoCliente { get; set; } = "Minoristas"; // "Minoristas" o "Mayoristas"
        #endregion
    }
}
