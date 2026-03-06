using AutoMapper;
using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Repositorio;
using FabricaPastas.Server.Servicios;
using FabricaPastas.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly Context _context;
        private readonly IPedidoRepositorio _repositorio;
        private readonly IMapper _mapper;

        public PedidoController(Context context, IPedidoRepositorio repositorio, IMapper mapper)
        {
            _context = context;
            _repositorio = repositorio;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> Get()
        {
            var pedidos = await _context.Pedido
                .Include(p => p.Detalles)
                .Include(p => p.Usuario)
                    .ThenInclude(u => u.Tipo_Cliente)
                .ToListAsync();

            return Ok(pedidos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pedido>> Get(int id)
        {
            var pedido = await _context.Pedido
                .Include(p => p.Detalles)
                .Include(p => p.Usuario)
                    .ThenInclude(u => u.Tipo_Cliente)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound("No se encontró el pedido solicitado.");

            return Ok(pedido);
        }

        // POST: api/Pedido
        [HttpPost]
        public async Task<ActionResult<int>> CrearPedido([FromBody] CrearPedidoDTO dto)
        {
            if (dto == null || dto.Productos == null || dto.Productos.Count == 0)
                return BadRequest("El pedido debe contener al menos un producto.");

            if (dto.Usuario_Id <= 0)
                return BadRequest("Usuario inválido. No se recibió Usuario_Id.");

            if (dto.Forma_Pago_Id <= 0) dto.Forma_Pago_Id = 1;
            if (dto.Metodo_Entrega_Id <= 0) dto.Metodo_Entrega_Id = 1;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // =========================
                // 1) Validación + lectura de stock real desde BD
                // =========================
                var ids = dto.Productos.Select(x => x.Producto_Id).Distinct().ToList();

                var productosDb = await _context.Producto
                    .Where(p => ids.Contains(p.Id))
                    .ToListAsync();

                if (productosDb.Count != ids.Count)
                    return BadRequest("Hay productos inválidos en el pedido.");

                foreach (var item in dto.Productos)
                {
                    var prod = productosDb.First(p => p.Id == item.Producto_Id);

                    if (!prod.Activo)
                        return BadRequest($"El producto '{prod.Nombre}' está inactivo.");

                    if (item.Cantidad <= 0)
                        return BadRequest($"Cantidad inválida para '{prod.Nombre}'.");

                    if (prod.Stock < item.Cantidad)
                        return BadRequest($"No hay stock suficiente para '{prod.Nombre}'. Stock: {prod.Stock}.");
                }

                // =========================
                // 2) Buscar descuentos por promociones activas (hoy) y por producto
                // =========================
                var hoy = DateOnly.FromDateTime(DateTime.Now);

                // max descuento por producto (si hay varias promos activas)
                var descuentos = await (
                    from promo in _context.Promocion
                    join pp in _context.Promocion_Producto on promo.Id equals pp.Promocion_Id
                    where promo.Activa
                          && promo.Fecha_Inicio <= hoy
                          && promo.Fecha_Fin >= hoy
                          && ids.Contains(pp.Producto_Id)
                    group promo by pp.Producto_Id into g
                    select new
                    {
                        ProductoId = g.Key,
                        Descuento = g.Max(x => x.Descuento_Porcentaje)
                    }
                ).ToDictionaryAsync(x => x.ProductoId, x => x.Descuento);

                // =========================
                // 3) Construir detalles con descuento aplicado
                // =========================
                var detalles = new List<Detalle_Pedido>();
                decimal totalFinal = 0;

                foreach (var item in dto.Productos)
                {
                    var descuentoPct = descuentos.TryGetValue(item.Producto_Id, out var d) ? d : 0m;
                    if (descuentoPct < 0) descuentoPct = 0;
                    if (descuentoPct > 100) descuentoPct = 100;

                    var subtotalBruto = item.Cantidad * item.Precio_Unitario;
                    var subtotalFinal = subtotalBruto * (1m - (descuentoPct / 100m));

                    detalles.Add(new Detalle_Pedido
                    {
                        Producto_Id = item.Producto_Id,
                        Nombre = item.Nombre,
                        Cantidad = item.Cantidad,
                        Precio_Unitario = item.Precio_Unitario,
                        Subtotal = Math.Round(subtotalFinal, 2)
                    });

                    totalFinal += Math.Round(subtotalFinal, 2);
                }

                // =========================
                // 4) Descontar stock en BD
                // =========================
                foreach (var item in dto.Productos)
                {
                    var prod = productosDb.First(p => p.Id == item.Producto_Id);
                    prod.Stock -= item.Cantidad;
                }

                // =========================
                // 5) Crear pedido
                // =========================
                var pedido = new Pedido
                {
                    Usuario_Id = dto.Usuario_Id,
                    Estado_Pedido_Id = 1,
                    Forma_Pago_Id = dto.Forma_Pago_Id,
                    Metodo_Entrega_Id = dto.Metodo_Entrega_Id,
                    FechaPedido = dto.Fecha_Pedido,
                    FechaEntrega = dto.Fecha_Entrega,
                    Total = Math.Round(totalFinal, 2),

                    CodigoPedido = string.IsNullOrWhiteSpace(dto.CodigoPedido)
                        ? Guid.NewGuid().ToString("N")[..8].ToUpper()
                        : dto.CodigoPedido,

                    ObservacionesCatering = dto.Observaciones_Catering,
                    Detalles = detalles
                };

                _context.Pedido.Add(pedido);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(pedido.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Error al guardar el pedido: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Pedido entidad)
        {
            if (id != entidad.Id)
                return BadRequest("El ID del pedido no coincide.");

            var pedidoExistente = await _context.Pedido.FindAsync(id);
            if (pedidoExistente == null)
                return NotFound("No se encontró el pedido.");

            pedidoExistente.FechaPedido = entidad.FechaPedido;
            pedidoExistente.FechaEntrega = entidad.FechaEntrega;
            pedidoExistente.Total = entidad.Total;
            pedidoExistente.ObservacionesCatering = entidad.ObservacionesCatering;
            pedidoExistente.Estado_Pedido_Id = entidad.Estado_Pedido_Id;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar el pedido: {e.Message}");
            }
        }

        [HttpPut("{id:int}/estado")]
        public async Task<ActionResult> CambiarEstado(int id, [FromBody] int estadoPedidoId)
        {
            var pedidoExistente = await _context.Pedido.FindAsync(id);
            if (pedidoExistente == null)
                return NotFound("No se encontró el pedido.");

            // Validar que el estado exista
            var existeEstado = await _context.Estado_Pedido.AnyAsync(e => e.Id == estadoPedidoId);
            if (!existeEstado)
                return BadRequest("Estado inválido.");

            pedidoExistente.Estado_Pedido_Id = estadoPedidoId;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id:int}/recibo")]
        public async Task<IActionResult> DescargarRecibo(int id)
        {
            var pedido = await _context.Pedido
                .Include(p => p.Detalles)
                .Include(p => p.Usuario)
                    .ThenInclude(u => u.Tipo_Cliente)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound("Pedido no encontrado.");

            var pdfServicio = new PdfServicio();
            var pdfBytes = pdfServicio.GenerarRecibo(pedido);

            return File(pdfBytes, "application/pdf", $"Recibo_{pedido.CodigoPedido}.pdf");
        }
    }
}
