using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Descripcion), IsUnique = true)]
    public class Estado_Pedido : EntityBase
    {
        #region Atributos
        [Required, MaxLength(50)]
        public string Descripcion { get; set; } = string.Empty;
        #endregion

        #region Navegación
        public ICollection<Pedido>? Pedidos { get; set; }
        #endregion
    }
}
