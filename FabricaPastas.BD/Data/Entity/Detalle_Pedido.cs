using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Pedido_Id))]
    [Index(nameof(Producto_Id))]
    public class Detalle_Pedido : EntityBase
    {
        #region Claves Foráneas
        public int Pedido_Id { get; set; }
        public int Producto_Id { get; set; }
        #endregion

        #region Atributos
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public int Cantidad { get; set; }

        public decimal Precio_Unitario { get; set; }

        public decimal Subtotal { get; set; }

        [MaxLength(200)]
        public string? Descripcion { get; set; }
        #endregion

        #region Navegación
        public Pedido? Pedido { get; set; }
        public Producto? Producto { get; set; }
        #endregion
    }
}
