using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearLoginDTO
    {
        //[Required(ErrorMessage = "El campo E-mail es obligatorio")]
        //[MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}")]
        //public string? Email { get; set; }

        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo Nombre de usuario es obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? NombreUsuario { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        [MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Contraseña { get; set; }
    }
}
