using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearMetodo_EntregaDTO
    {
        #region Atributos
        [Required(ErrorMessage = "El campo Descripción de la entrega es obligatorio")]
        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Descripcion { get; set; }
        #endregion
    }
}
