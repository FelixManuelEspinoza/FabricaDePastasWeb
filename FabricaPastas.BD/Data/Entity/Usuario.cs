using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.BD.Data.Entity
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(NombreUsuario), IsUnique = true)]
    public class Usuario : EntityBase
    {
        #region Claves foráneas
        public int Rol_Id { get; set; }
        public int Tipo_Cliente_Id { get; set; }
        public int Lista_Precio_Id { get; set; }
        #endregion

        #region Atributos
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Apellido { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Telefono { get; set; }

        [MaxLength(150)]
        public string? Direccion { get; set; }

        [MaxLength(20)]
        public string? Cuit_Cuil { get; set; }
        #endregion

        #region Navegación
        [ForeignKey(nameof(Rol_Id))]
        public Rol? Rol { get; set; }

        [ForeignKey(nameof(Tipo_Cliente_Id))]
        public Tipo_Cliente? Tipo_Cliente { get; set; }

        [ForeignKey(nameof(Lista_Precio_Id))]
        public Lista_Precio? Lista_Precio { get; set; }

        public List<Pedido>? Pedidos { get; set; }
        #endregion
    }
}
