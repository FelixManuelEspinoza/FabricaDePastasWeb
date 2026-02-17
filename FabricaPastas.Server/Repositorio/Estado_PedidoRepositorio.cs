using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class Estado_PedidoRepositorio : Repositorio<Estado_Pedido>, IEstado_PedidoRepositorio
    {
        public Estado_PedidoRepositorio(Context context) : base(context)
        {
        }
    }
   
}
