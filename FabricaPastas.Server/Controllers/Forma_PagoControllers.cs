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
    [Route("api/Forma_Pago")]
    public class Forma_PagoControllers : ControllerBase
    {
        private readonly IForma_PagoRepositorio repositorio;

        //private readonly Context context;
        private readonly IMapper mapper;

        #region constructor
        public Forma_PagoControllers(IForma_PagoRepositorio repositorio,
                                     IMapper mapper)
        {
            this.repositorio = repositorio;
            //this.context = context;
            this.mapper = mapper;
        }
        #endregion

        #region Método Get
        [HttpGet]
        public async Task<ActionResult<List<Forma_Pago>>> Get()
        {
            return await repositorio.Select();
        }
        #endregion

        #region Método Post
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearForma_PagoDTO entidadDTO)
        {
            try
            {
                

                Forma_Pago entidad = mapper.Map<Forma_Pago>(entidadDTO);


                return await repositorio.Insert(entidad);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #endregion

    }
}
        

        



        
