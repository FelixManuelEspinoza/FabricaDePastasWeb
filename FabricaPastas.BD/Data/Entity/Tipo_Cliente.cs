using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Descripcion), IsUnique = true)]
    public class Tipo_Cliente : EntityBase
    {
        [Required, MaxLength(50)]
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
