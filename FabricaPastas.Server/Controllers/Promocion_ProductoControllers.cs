using AutoMapper;
using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Repositorio;
using FabricaPastas.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Promocion_Producto")]
    public class Promocion_ProductoControllers : ControllerBase
    {
        private readonly IPromocion_ProductoRepositorio repositorio;

        //private readonly Context context;
        private readonly IMapper mapper;

        #region constructor
        public Promocion_ProductoControllers(IPromocion_ProductoRepositorio repositorio,
                                             IMapper mapper)
        {
            this.repositorio = repositorio;
            //this.context = context;
            this.mapper = mapper;   
        }
        #endregion

        #region Método Get
        [HttpGet]
        public async Task<ActionResult<List<Promocion_Producto>>> Get()
        {
            return await repositorio.Select();
        }
        #endregion



        #region Método Post
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearPromocion_ProductoDTO entidadDTO)
        {
            try
            {
                //Usuario entidad = new Usuario();

                //entidad.Nombre = entidadDTO.Nombre;
                //entidad.Apellido = entidadDTO.Apellido;
                //entidad.Email = entidadDTO.Email;
                //entidad.Contraseña = entidadDTO.Contraseña;
                //entidad.Teléfono = entidadDTO.Teléfono;
                //entidad.Dirección = entidadDTO.Dirección;
                //entidad.Cuit_Cuil = entidadDTO.Cuit_Cuil;
                //entidad.Fecha_Registro = entidadDTO.Fecha_Registro;

                Promocion_Producto entidad = mapper.Map<Promocion_Producto>(entidadDTO);


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
                return NotFound($"La promocion del producto {id} no existe.");
            }
            if (await repositorio.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest("No se pudo eliminar la promoción del producto");
            }
        }
        #endregion
    }
}
