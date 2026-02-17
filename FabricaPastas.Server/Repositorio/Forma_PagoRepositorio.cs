using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class Forma_PagoRepositorio : Repositorio<Forma_Pago>, IForma_PagoRepositorio
    {
        public Forma_PagoRepositorio(Context context) : base(context)
        {
        }
    }
}
