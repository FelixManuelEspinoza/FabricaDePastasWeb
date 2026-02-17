using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabricaPastas.Shared.DTO
{
    public class LoginRespuestaDTO
    {
        public bool Exitoso { get; set; }
        public string? Mensaje { get; set; }

        // opcional si luego usás JWT
        public string? Token { get; set; }

        // ✅ NUEVO: lo que necesitamos para pedidos y filtros
        public int UsuarioId { get; set; }
        public string? NombreUsuario { get; set; }
        public string? TipoCliente { get; set; } // "Mayorista" / "Minorista"
        public string? Rol { get; set; }         // "Administrador" / "Usuario"
    }
}
