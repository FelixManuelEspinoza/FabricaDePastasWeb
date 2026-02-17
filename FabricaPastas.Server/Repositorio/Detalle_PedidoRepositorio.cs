using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class Detalle_PedidoRepositorio : Repositorio<Detalle_Pedido>, IDetalle_PedidoRepositorio
    {
        public Detalle_PedidoRepositorio(Context context) : base(context)
        {
        }
    }
  
}
