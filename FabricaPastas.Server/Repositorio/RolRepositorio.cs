using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class RolRepositorio : Repositorio<Rol>, IRolRepositorio
    {
        public RolRepositorio(Context context) : base(context)
        {
        }
    }

}
