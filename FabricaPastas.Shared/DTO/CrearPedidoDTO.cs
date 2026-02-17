using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.Shared.DTO
{
    public class CrearPedidoDTO
    {
        [Required(ErrorMessage = "La fecha del pedido es obligatoria")]
        public DateTime Fecha_Pedido { get; set; }

        [Required(ErrorMessage = "La fecha de entrega es obligatoria")]
        public DateTime Fecha_Entrega { get; set; }

        // 🔹 Observaciones generales del pedido (comentarios del cliente)
        public string? Observaciones { get; set; }

        // 🔹 Observaciones específicas de catering (opcional)
        public string? Observaciones_Catering { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El total no puede ser negativo")]
        public decimal Total { get; set; }

        // ✅ Lista de productos dentro del pedido
        public List<CrearDetalle_PedidoDTO> Productos { get; set; } = new();

        // ✅ Información del usuario que hace el pedido (cliente)
        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        public int Usuario_Id { get; set; }

        // ✅ Identificador del método de pago elegido
        [Required(ErrorMessage = "Debe seleccionar una forma de pago")]
        public int Forma_Pago_Id { get; set; }

        // ✅ Identificador del método de entrega (retiro, envío, etc.)
        [Required(ErrorMessage = "Debe seleccionar un método de entrega")]
        public int Metodo_Entrega_Id { get; set; }

        // 🔹 Descripción del método de pago (efectivo, tarjeta, transferencia, etc.)
        public string? MetodoPago { get; set; }

        // 🔹 Código único para rastrear el pedido
        public string CodigoPedido { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();

        // 🔹 Estado inicial del pedido
        public string Estado { get; set; } = "Pendiente";
    }
}
