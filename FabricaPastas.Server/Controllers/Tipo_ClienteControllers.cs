using AutoMapper;
using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Tipo_Cliente")]
    public class Tipo_ClienteControllers : ControllerBase
    {
        private readonly ITipo_ClienteRepositorio repositorio;

        //private readonly Context context;

        #region constructor
        public Tipo_ClienteControllers(ITipo_ClienteRepositorio repositorio,
                                       IMapper mapper)
        {
            this.repositorio = repositorio;
            //this.context = context;
            this.Mapper = mapper;
        }

        public IMapper Mapper { get; }
        #endregion

        #region Método Get
        [HttpGet]
        public async Task<ActionResult<List<Tipo_Cliente>>> Get()
        {
            return await repositorio.Select();
        }
        #endregion

        #region Método Post

        [HttpPost]
        public async Task<ActionResult<int>> Post(Tipo_Cliente entidad)
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
                return NotFound($"El Tipo de cliente {id} no existe.");
            }
            if (await repositorio.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest("No se pudo eliminar el tipo de cliente");
            }
        }
        #endregion

    }
}



       

       

        

        

       

        
