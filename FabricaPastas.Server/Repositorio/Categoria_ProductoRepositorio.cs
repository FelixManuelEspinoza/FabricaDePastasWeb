using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;

namespace FabricaPastas.Server.Repositorio
{
    public class Categoria_ProductoRepositorio : Repositorio<Categoria_Producto>, ICategoria_ProductoRepositorio
    {
        public Categoria_ProductoRepositorio(Context context) : base(context)
        {
        }
    }
  
}
