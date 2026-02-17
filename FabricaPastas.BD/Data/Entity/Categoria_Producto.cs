using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class Categoria_Producto : EntityBase
    {
        #region Atributos
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        public bool Activa { get; set; } = true;
        #endregion

        #region Navegación
        public ICollection<Producto>? Productos { get; set; }
        #endregion
    }
}
