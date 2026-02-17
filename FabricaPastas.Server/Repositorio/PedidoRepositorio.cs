using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class PedidoRepositorio : Repositorio<Pedido>, IPedidoRepositorio
    {
        public PedidoRepositorio(Context context) : base(context)
        {
        }
    }
   
}
