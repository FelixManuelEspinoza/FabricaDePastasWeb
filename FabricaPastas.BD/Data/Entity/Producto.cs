using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Nombre))]
    [Index(nameof(CategoriaProductoId))]
    public class Producto : EntityBase
    {
        #region Claves foráneas
        public int CategoriaProductoId { get; set; }
        #endregion

        #region Atributos
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Descripcion { get; set; }

        public decimal PrecioBase { get; set; }

        public string? ImagenUrl { get; set; }

        public int Stock { get; set; }

        public bool Activo { get; set; } = true;
        #endregion

        #region Navegación
        public Categoria_Producto? Categoria { get; set; }
        #endregion
    }
}
