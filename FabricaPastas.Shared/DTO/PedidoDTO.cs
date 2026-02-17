using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.Shared.DTO
{
    public class PedidoDTO
    {
        public int Pedido_Id { get; set; }

        public int Usuario_Id { get; set; }

        public int Estado_Pedido_Id { get; set; }

        public int Forma_Pago_Id { get; set; }

        public int Metodo_Entrega_Id { get; set; }

        [Required(ErrorMessage = "La fecha del pedido es obligatoria")]
        public DateTime Fecha_Pedido { get; set; }

        [Required(ErrorMessage = "La fecha de entrega es obligatoria")]
        public DateTime Fecha_Entrega { get; set; }

        public string? Observaciones_Catering { get; set; }

        public decimal Total { get; set; }

        // Lista de productos asociados al pedido
        public List<CrearDetalle_PedidoDTO> Productos { get; set; } = new();
    }
}
