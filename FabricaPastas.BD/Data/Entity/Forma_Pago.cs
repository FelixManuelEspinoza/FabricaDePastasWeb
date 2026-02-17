using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Metodo), IsUnique = true)]
    public class Forma_Pago : EntityBase
    {
        [Required, MaxLength(30)]
        public string Metodo { get; set; } = string.Empty;

        public ICollection<Pedido>? Pedidos { get; set; }
    }
}
