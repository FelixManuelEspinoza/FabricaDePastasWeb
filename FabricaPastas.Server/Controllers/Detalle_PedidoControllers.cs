//using AutoMapper;
//using FabricaPastas.BD.Data.Entity;
//using FabricaPastas.Server.Repositorio;
//using FabricaPastas.Shared.DTO;
//using Microsoft.AspNetCore.Mvc;

//namespace FabricaPastas.Server.Controllers
//{
//    [ApiController]
//    [Route("api/Detalle_Pedido")]
//    public class Detalle_PedidoControllers : ControllerBase
//    {
//        private readonly IDetalle_PedidoRepositorio repositorio;
//        private readonly IMapper mapper;

//        #region Constructor
//        public Detalle_PedidoControllers(
//            IDetalle_PedidoRepositorio repositorio,
//            IMapper mapper)
//        {
//            this.repositorio = repositorio;
//            this.mapper = mapper;
//        }
//        #endregion

//        #region GET
//        [HttpGet]
//        public async Task<ActionResult<List<Detalle_Pedido>>> Get()
//        {
//            return await repositorio.Select();
//        }
//        #endregion

//        #region POST
//        [HttpPost]
//        public async Task<ActionResult<int>> Post(CrearDetalle_PedidoDTO entidadDTO)
//        {
//            try
//            {
//                var entidad = mapper.Map<Detalle_Pedido>(entidadDTO);

//                // seguridad mínima: recalcular subtotal
//                entidad.Subtotal = entidad.Cantidad * entidad.Precio_Unitario;

//                return await repositorio.Insert(entidad);
//            }
//            catch (Exception e)
//            {
//                return BadRequest(e.Message);
//            }
//        }
//        #endregion
//    }
//}
