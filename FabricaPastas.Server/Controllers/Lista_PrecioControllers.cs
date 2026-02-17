using AutoMapper;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Repositorio;
using FabricaPastas.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Lista_Precio")]
    public class Lista_PrecioControllers : ControllerBase
    {
        private readonly ILista_PrecioRepositorio repositorio;
        private readonly IMapper mapper;

        public Lista_PrecioControllers(
            ILista_PrecioRepositorio repositorio,
            IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        #region GET
        [HttpGet]
        public async Task<ActionResult<List<Lista_Precio>>> Get()
        {
            return await repositorio.Select();
        }
        #endregion

        #region GET by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Lista_Precio>> Get(int id)
        {
            var entidad = await repositorio.SelectById(id);

            if (entidad == null)
                return NotFound();

            return entidad;
        }
        #endregion

        #region POST (ADMIN)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<int>> Post(CrearLista_PrecioDTO dto)
        {
            try
            {
                var entidad = mapper.Map<Lista_Precio>(dto);
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
        public async Task<ActionResult> Put(int id, [FromBody] Lista_Precio entidad)
        {
            if (id != entidad.Id)
                return BadRequest("Datos incorrectos");

            var existente = await repositorio.SelectById(id);
            if (existente == null)
                return NotFound("No se encontró la lista de precio");

            existente.Tipo = entidad.Tipo;

            try
            {
                await repositorio.Update(id, existente);
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
            var existe = await repositorio.Existe(id);

            if (!existe)
                return NotFound($"La lista de precio {id} no existe.");

            if (await repositorio.Delete(id))
                return Ok();

            return BadRequest("No se pudo eliminar la lista de precio");
        }
        #endregion
    }
}
