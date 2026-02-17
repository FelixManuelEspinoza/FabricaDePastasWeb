using FabricaPastas.BD.Data;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.Server.Repositorio
{
    public class Repositorio<E> : IRepositorio<E> 
                    where E : class, IEntityBase
    {
        private readonly Context context;

        #region constructor
        public Repositorio(Context context)
        {
            this.context = context;
        }
        #endregion

        #region Método Get
        public async Task<List<E>> Select()
        {
            return await context.Set<E>().ToListAsync();
        }
        #endregion

        #region Existe for Id
        public async Task<bool> Existe(int id)
        {
            var existe = await context.Set<E>().AnyAsync(x => x.Id == id);
            return existe;
        }
        #endregion

        #region Post
        public async Task<int> Insert(E entidad)
        {
            try
            {
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception err)
            {

                throw err;
            }
        }
        #endregion

        #region Update
        public async Task<bool> Update(int id, E entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }

            var lean = await SelectById(id);

            if (lean == null)
            {
                return false;
            }

            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        #endregion

        #region SelectById
        public async Task<E> SelectById(int id)
        {
            E? lean = await context.Set<E>().AsNoTracking() // AsNoTracking() == (Para evitar que no quede en memoria)
                .FirstOrDefaultAsync(x => x.Id == id);

            return lean;
        }
        #endregion

        #region Borrar
        public async Task<bool> Delete(int id)
        {
            var pepe = await SelectById(id);

            if (pepe == null)
            {
                return false;
            }

            context.Set<E>().Remove(pepe);
            await context.SaveChangesAsync();
            return true;
        }
        #endregion


        public IQueryable<E> Query()
        {
            return context.Set<E>().AsQueryable();
        }

    }
}
