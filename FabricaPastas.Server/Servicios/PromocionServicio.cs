using FabricaPastas.BD.Data;
using FabricaPastas.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.Server.Servicios
{
    public interface IPromocionServicio
    {
        Task<DescuentoActualDTO> ObtenerDescuentoActual(int usuarioId);
    }

    public class PromocionServicio : IPromocionServicio
    {
        private readonly Context _context;

        public PromocionServicio(Context context)
        {
            _context = context;
        }

        public async Task<DescuentoActualDTO> ObtenerDescuentoActual(int usuarioId)
        {
            var usuario = await _context.Usuario
                .AsNoTracking()
                .Include(u => u.Tipo_Cliente)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null)
            {
                return new DescuentoActualDTO { UsuarioId = usuarioId, CantidadPedidos = 0, DescuentoPorcentaje = 0 };
            }

            // Contamos pedidos NO cancelados (Estado 5 según tu seeder)
            var pedidosCount = await _context.Pedido
                .AsNoTracking()
                .CountAsync(p => p.Usuario_Id == usuarioId && p.Estado_Pedido_Id != 5);

            var hoy = DateOnly.FromDateTime(DateTime.Now);

            // Buscamos promos activas hoy + rangos que coincidan
            var candidatos = await _context.Promocion
                .AsNoTracking()
                .Where(p => p.Activa && p.Fecha_Inicio <= hoy && p.Fecha_Fin >= hoy)
                .SelectMany(p => p.Rangos!.Select(r => new
                {
                    PromocionId = p.Id,
                    PromocionTitulo = p.Titulo,
                    r.Tipo_Cliente_Id,
                    r.MinPedidos,
                    r.MaxPedidos,
                    r.Descuento_Porcentaje
                }))
                .ToListAsync();

            // filtrar por tipo cliente + rango pedidos
            var aplica = candidatos
                .Where(r =>
                    (r.Tipo_Cliente_Id == null || r.Tipo_Cliente_Id == usuario.Tipo_Cliente_Id) &&
                    pedidosCount >= r.MinPedidos &&
                    (r.MaxPedidos == null || pedidosCount <= r.MaxPedidos.Value))
                .OrderByDescending(r => r.Descuento_Porcentaje)
                .FirstOrDefault();

            if (aplica == null)
            {
                return new DescuentoActualDTO
                {
                    UsuarioId = usuarioId,
                    CantidadPedidos = pedidosCount,
                    DescuentoPorcentaje = 0,
                    PromocionId = null,
                    PromocionTitulo = null
                };
            }

            return new DescuentoActualDTO
            {
                UsuarioId = usuarioId,
                CantidadPedidos = pedidosCount,
                DescuentoPorcentaje = aplica.Descuento_Porcentaje,
                PromocionId = aplica.PromocionId,
                PromocionTitulo = aplica.PromocionTitulo
            };
        }
    }
}
