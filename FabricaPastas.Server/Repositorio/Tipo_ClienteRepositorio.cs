using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class Tipo_ClienteRepositorio : Repositorio<Tipo_Cliente>, ITipo_ClienteRepositorio
    {
        public Tipo_ClienteRepositorio(Context context) : base(context)
        {
        }
    }
}
