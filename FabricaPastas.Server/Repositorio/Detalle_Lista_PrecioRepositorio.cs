using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class Detalle_Lista_PrecioRepositorio : Repositorio<Detalle_Lista_Precio>, IDetalle_Lista_PrecioRepositorio
    {
        public Detalle_Lista_PrecioRepositorio(Context context) : base(context)
        {
        }
    }
   
}
