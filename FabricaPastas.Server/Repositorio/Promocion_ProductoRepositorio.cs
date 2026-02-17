using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class Promocion_ProductoRepositorio : Repositorio<Promocion_Producto>, IPromocion_ProductoRepositorio
    {
        public Promocion_ProductoRepositorio(Context context) : base(context)
        {
        }
    }
}
