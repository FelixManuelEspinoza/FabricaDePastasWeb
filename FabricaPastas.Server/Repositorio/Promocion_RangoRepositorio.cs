using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class Promocion_RangoRepositorio : Repositorio<Promocion_Rango>, IPromocion_RangoRepositorio
    {
        public Promocion_RangoRepositorio(Context context) : base(context) { }
    }
}
