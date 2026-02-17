using AutoMapper;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Repositorio;
using FabricaPastas.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Categoria_Producto")]
    public class Categoria_ProductoControllers : ControllerBase
    {
        private readonly ICategoria_ProductoRepositorio repositorio;
        private readonly IMapper mapper;

        #region Constructor
        public Categoria_ProductoControllers(
            ICategoria_ProductoRepositorio repositorio,
            IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }
        #endregion

        #region GET
        [HttpGet]
        public async Task<ActionResult<List<Categoria_Producto>>> Get()
        {
            return await repositorio.Select();
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearCategoria_ProductoDTO entidadDTO)
        {
            try
            {
                var entidad = mapper.Map<Categoria_Producto>(entidadDTO);
                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Categoria_Producto entidad)
        {
            if (id != entidad.Id)
                return BadRequest("Datos incorrectos");

            var existente = await repositorio.SelectById(id);

            if (existente == null)
                return NotFound("No se encontró la categoría buscada");

            existente.Nombre = entidad.Nombre;
            existente.Descripcion = entidad.Descripcion;
            existente.Activa = entidad.Activa;

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

        #region DELETE
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);

            if (!existe)
                return NotFound($"La categoría {id} no existe.");

            var eliminado = await repositorio.Delete(id);

            return eliminado
                ? Ok()
                : BadRequest("No se pudo eliminar la categoría");
        }
        #endregion
    }
}
