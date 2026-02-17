using AutoMapper;
using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Estado_Pedido")]
    public class Estado_PedidoControllers : ControllerBase
    {
        private readonly IEstado_PedidoRepositorio repositorio;

        //private readonly Context context;
        private readonly IMapper mapper;

        #region constructor
        public Estado_PedidoControllers(IEstado_PedidoRepositorio repositorio,
                                        IMapper mapper)
        {
            this.repositorio = repositorio;
            //this.context = context;
            this.mapper = mapper;
        }
        #endregion

        #region Método Get
        [HttpGet]
        public async Task<ActionResult<List<Estado_Pedido>>> Get()
        {
            return await repositorio.Select();
        }
        #endregion


    }
}

        
