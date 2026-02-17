using AutoMapper;
using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Rol")]
    public class RolControllers : ControllerBase
    {
        private readonly IRolRepositorio repositorio;

        //private readonly Context context;
        private readonly IMapper mapper;

        #region constructor
        public RolControllers(IRolRepositorio repositorio,
                              IMapper mapper)
        {
            this.repositorio = repositorio;
            //this.context = context;
            this.mapper = mapper;
        }
        #endregion

        #region Método Get
        [HttpGet]
        public async Task<ActionResult<List<Rol>>> Get()
        {
            return await repositorio.Select();
        }
        #endregion

        #region Método Post
        [HttpPost]
        public async Task<ActionResult<int>> Post(Rol entidad)
        {
            try
            {

                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Método Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);

            if (!existe)
            {
                return NotFound($"El Rol {id} no existe.");
            }
            if (await repositorio.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest("No se pudo eliminar el rol");
            }
        }
        #endregion



    }
}

