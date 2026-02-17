using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(CodigoPedido), IsUnique = true)]
    [Index(nameof(FechaPedido))]
    [Index(nameof(Usuario_Id))]
    public class Pedido : EntityBase
    {
        #region Claves Foráneas
        public int Usuario_Id { get; set; }
        public int Estado_Pedido_Id { get; set; }
        public int Metodo_Entrega_Id { get; set; }
        public int Forma_Pago_Id { get; set; }
        #endregion

        #region Atributos
        [Required]
        public DateTime FechaPedido { get; set; } = DateTime.Now;

        public DateTime? FechaEntrega { get; set; }

        public string? ObservacionesCatering { get; set; }

        public decimal Total { get; set; }

        [MaxLength(20)]
        public string CodigoPedido { get; set; } =
            Guid.NewGuid().ToString("N")[..8].ToUpper();
        #endregion

        #region Navegación
        public Usuario? Usuario { get; set; }
        public Estado_Pedido? Estado_Pedido { get; set; }
        public Metodo_Entrega? Metodo_Entrega { get; set; }
        public Forma_Pago? Forma_Pago { get; set; }

        public List<Detalle_Pedido> Detalles { get; set; } = new();
        #endregion
    }
}
