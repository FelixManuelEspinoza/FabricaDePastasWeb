using AutoMapper;
using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Repositorio;
using FabricaPastas.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Detalle_Lista_Precio")]
    public class Detalle_Lista_PrecioControllers : ControllerBase
    {
        private readonly IDetalle_Lista_PrecioRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly Context _context;

        public Detalle_Lista_PrecioControllers(
            IDetalle_Lista_PrecioRepositorio repositorio,
            IMapper mapper,
            Context context)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            _context = context;
        }

        #region GET (PUBLICO porque lo usa CatalogoUsuario)
        [HttpGet]
        public async Task<ActionResult<List<Detalle_Lista_Precio>>> Get()
        {
            var lista = await _context.Detalle_Lista_Precio
                .Include(d => d.Lista_Precio)
                .Include(d => d.Producto)
                .ToListAsync();

            return Ok(lista);
        }
        #endregion

        #region POST (ADMIN)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<int>> Post([FromBody] CrearDetalle_Lista_PrecioDTO dto)
        {
            try
            {
                var entidad = mapper.Map<Detalle_Lista_Precio>(dto);
                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region PUT (ADMIN)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Put(int id, [FromBody] decimal precio)
        {
            var entidad = await repositorio.SelectById(id);

            if (entidad == null)
                return NotFound("No se encontró el detalle de lista de precio.");

            entidad.Precio = precio;

            try
            {
                await repositorio.Update(id, entidad);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region DELETE (ADMIN)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await repositorio.Existe(id))
                return NotFound($"El detalle de lista {id} no existe.");

            return await repositorio.Delete(id)
                ? Ok()
                : BadRequest("El detalle no se pudo eliminar");
        }
        #endregion
    }
}
