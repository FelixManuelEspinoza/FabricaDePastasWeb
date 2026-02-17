using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.Shared.DTO
{
    public class CrearDetalle_PedidoDTO
    {
        #region Atributos 

        // ✅ Identificador del producto asociado
        [Required(ErrorMessage = "Debe especificarse el producto")]
        public int Producto_Id { get; set; }

        [Required(ErrorMessage = "El campo nombre del producto es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Cantidad es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo Precio Unitario es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que cero")]
        public decimal Precio_Unitario { get; set; }

        // 🔹 Descripción opcional (coincide con la entidad)
        [MaxLength(200)]
        public string? Descripcion { get; set; }

        // 🔹 Subtotal calculado (no mapeado)
        public decimal Subtotal => Cantidad * Precio_Unitario;

        #endregion
    }
}
