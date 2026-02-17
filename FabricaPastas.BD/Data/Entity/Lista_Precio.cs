using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Tipo), IsUnique = true)]
    public class Lista_Precio : EntityBase
    {
        [Required, MaxLength(30)]
        public string Tipo { get; set; } = string.Empty;
        // "Minorista" / "Mayorista"

        public ICollection<Detalle_Lista_Precio>? Detalles { get; set; }
    }
}
