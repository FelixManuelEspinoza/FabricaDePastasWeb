using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class Rol : EntityBase
    {
        #region Atributos
        [Required, MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;
        #endregion

        #region Navegación
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        #endregion
    }
}
