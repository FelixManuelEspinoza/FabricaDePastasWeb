using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class CrearEstado_PedidoDTO
    {
        #region Atributos
        [Required(ErrorMessage = "El campo Descripcion del pedido es obligatorio")]
        [MaxLength(255, ErrorMessage = "Máximo número de caracteres {1}")]
        public string? Descripcion { get; set; }
        #endregion
    }
}
