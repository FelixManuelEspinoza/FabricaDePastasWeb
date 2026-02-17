using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class Metodo_EntregaRepositorio : Repositorio<Metodo_Entrega>, IMetodo_EntregaRepositorio
    {
        public Metodo_EntregaRepositorio(Context context) : base(context)
        {
        }
    }
  
}
