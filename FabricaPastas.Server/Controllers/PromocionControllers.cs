using AutoMapper;
using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Servicios;
using FabricaPastas.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Promocion")]
    public class PromocionControllers : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IPromocionServicio _promoServicio;

        public PromocionControllers(Context context, IMapper mapper, IPromocionServicio promoServicio)
        {
            _context = context;
            _mapper = mapper;
            _promoServicio = promoServicio;
        }

        // ============================
        // PUBLICO (para USER logueado): descuento actual
        // GET api/Promocion/DescuentoActual
        // ============================
        [HttpGet("DescuentoActual")]
        [Authorize] // cualquier usuario logueado
        public async Task<ActionResult<DescuentoActualDTO>> DescuentoActual()
        {
            var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idStr, out var userId) || userId <= 0)
                return Unauthorized();

            var dto = await _promoServicio.ObtenerDescuentoActual(userId);
            return Ok(dto);
        }

        // ============================
        // ADMIN CRUD
        // ============================

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<List<Promocion>>> Get()
        {
            var lista = await _context.Promocion
                .Include(p => p.Rangos)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            return Ok(lista);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Promocion>> Get(int id)
        {
            var promo = await _context.Promocion
                .Include(p => p.Rangos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (promo == null) return NotFound();
            return Ok(promo);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<int>> Post([FromBody] CrearPromocionDTO dto)
        {
            var entidad = _mapper.Map<Promocion>(dto);
            _context.Promocion.Add(entidad);
            await _context.SaveChangesAsync();
            return Ok(entidad.Id);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Put(int id, [FromBody] CrearPromocionDTO dto)
        {
            var existente = await _context.Promocion.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Titulo = dto.Titulo ?? "";
            existente.Descripcion = dto.Descripcion;
            existente.Fecha_Inicio = dto.Fecha_Inicio;
            existente.Fecha_Fin = dto.Fecha_Fin;
            existente.Activa = dto.Activa;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Promocion.FindAsync(id);
            if (existe == null) return NotFound();

            _context.Promocion.Remove(existe);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
