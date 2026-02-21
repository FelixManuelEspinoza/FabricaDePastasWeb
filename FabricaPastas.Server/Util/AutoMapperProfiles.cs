using AutoMapper;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Shared.DTO;

namespace FabricaPastas.Server.Util
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region DTO Categoria_Producto
            CreateMap<CrearCategoria_ProductoDTO, Categoria_Producto>();
            CreateMap<Categoria_Producto, CrearCategoria_ProductoDTO>();
            #endregion

            #region DTO Detalle_Lista_Precio
            CreateMap<CrearDetalle_Lista_PrecioDTO, Detalle_Lista_Precio>();
            CreateMap<Detalle_Lista_Precio, CrearDetalle_Lista_PrecioDTO>();
            #endregion

            #region DTO Detalle_Pedido
            CreateMap<CrearDetalle_PedidoDTO, Detalle_Pedido>();
            CreateMap<Detalle_Pedido, CrearDetalle_PedidoDTO>();
            #endregion

            #region DTO Estado_Pedido
            CreateMap<CrearEstado_PedidoDTO, Estado_Pedido>();
            CreateMap<Estado_Pedido, CrearEstado_PedidoDTO>();
            #endregion

            #region DTO Forma_Pago
            CreateMap<CrearForma_PagoDTO, Forma_Pago>();
            CreateMap<Forma_Pago, CrearForma_PagoDTO>();
            #endregion

            #region DTO Lista_Precio
            CreateMap<CrearLista_PrecioDTO, Lista_Precio>();
            CreateMap<Lista_Precio, CrearLista_PrecioDTO>();
            #endregion

            #region DTO Metodo_Entrega
            CreateMap<CrearMetodo_EntregaDTO, Metodo_Entrega>();
            CreateMap<Metodo_Entrega, CrearMetodo_EntregaDTO>();
            #endregion

            #region DTO Pedido
            CreateMap<CrearPedidoDTO, Pedido>();
            CreateMap<Pedido, CrearPedidoDTO>();
            #endregion

            #region DTO Producto
            CreateMap<CrearProductoDTO, Producto>();
            CreateMap<Producto, CrearProductoDTO>();
            #endregion

            #region DTO Promocion
            CreateMap<CrearPromocionDTO, Promocion>();
            CreateMap<Promocion, CrearPromocionDTO>();
            #endregion

            // ✅ NUEVO: rangos por cantidad de pedidos
            #region DTO Promocion_Rango
            CreateMap<CrearPromocion_RangoDTO, Promocion_Rango>();
            CreateMap<Promocion_Rango, CrearPromocion_RangoDTO>();
            #endregion

            // DTO -> Entity (registro)
            //CreateMap<CrearUsuarioDTO, Usuario>()
            //    .ForMember(d => d.Telefono, opt => opt.MapFrom(s => s.Teléfono))
            //    .ForMember(d => d.Direccion, opt => opt.MapFrom(s => s.Dirección))
            //    .ForMember(d => d.PasswordHash, opt => opt.Ignore());

            #region DTO Rol
            CreateMap<CrearRolDTO, Rol>();
            CreateMap<Rol, CrearRolDTO>();
            #endregion

            #region DTO Tipo_Cliente
            CreateMap<CrearTipo_ClienteDTO, Tipo_Cliente>();
            CreateMap<Tipo_Cliente, CrearTipo_ClienteDTO>();
            #endregion

            #region DTO Usuario
            CreateMap<CrearUsuarioDTO, Usuario>();
            CreateMap<Usuario, CrearUsuarioDTO>();
            #endregion

            #region DTO Login
            CreateMap<CrearLoginDTO, Usuario>()
                .ForMember(dest => dest.PasswordHash,
                           opt => opt.MapFrom(src => src.Contraseña));
            #endregion
        }
    }
}
