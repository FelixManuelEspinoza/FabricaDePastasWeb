using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class PromocionRepositorio : Repositorio<Promocion>, IPromocionRepositorio
    {
        public PromocionRepositorio(Context context) : base(context)
        {
        }
    }

}
