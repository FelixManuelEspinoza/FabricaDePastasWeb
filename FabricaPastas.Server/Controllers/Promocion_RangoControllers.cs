using AutoMapper;
using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Promocion_Rango")]
    [Authorize(Roles = "Administrador")]
    public class Promocion_RangoControllers : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public Promocion_RangoControllers(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Promocion_Rango/PorPromocion/5
        [HttpGet("PorPromocion/{promocionId:int}")]
        public async Task<ActionResult<List<Promocion_Rango>>> PorPromocion(int promocionId)
        {
            var lista = await _context.Promocion_Rango
                .Include(r => r.Tipo_Cliente)
                .Where(r => r.Promocion_Id == promocionId)
                .OrderBy(r => r.MinPedidos)
                .ToListAsync();

            return Ok(lista);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearPromocion_RangoDTO dto)
        {
            if (dto.MaxPedidos.HasValue && dto.MaxPedidos.Value < dto.MinPedidos)
                return BadRequest("MaxPedidos no puede ser menor que MinPedidos.");

            var entidad = _mapper.Map<Promocion_Rango>(dto);

            _context.Promocion_Rango.Add(entidad);
            await _context.SaveChangesAsync();

            return Ok(entidad.Id);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CrearPromocion_RangoDTO dto)
        {
            var existente = await _context.Promocion_Rango.FindAsync(id);
            if (existente == null) return NotFound();

            if (dto.MaxPedidos.HasValue && dto.MaxPedidos.Value < dto.MinPedidos)
                return BadRequest("MaxPedidos no puede ser menor que MinPedidos.");

            existente.Promocion_Id = dto.Promocion_Id;
            existente.Tipo_Cliente_Id = dto.Tipo_Cliente_Id;
            existente.MinPedidos = dto.MinPedidos;
            existente.MaxPedidos = dto.MaxPedidos;
            existente.Descuento_Porcentaje = dto.Descuento_Porcentaje;

            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existente = await _context.Promocion_Rango.FindAsync(id);
            if (existente == null) return NotFound();

            _context.Promocion_Rango.Remove(existente);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
