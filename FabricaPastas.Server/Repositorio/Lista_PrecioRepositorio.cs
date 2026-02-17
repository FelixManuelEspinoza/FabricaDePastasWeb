using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class Lista_PrecioRepositorio : Repositorio<Lista_Precio>, ILista_PrecioRepositorio
    {
        public Lista_PrecioRepositorio(FabricaPastas.BD.Data.Context context) : base(context)
        {
        }
    }
    
}
