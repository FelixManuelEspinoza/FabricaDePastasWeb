using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FabricaPastas.Server.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(Context context)
        {
            // =========================
            // ROLES
            // =========================
            if (!await context.Rol.AnyAsync())
            {
                context.Rol.AddRange(
                    new Rol { Nombre = "Administrador" },
                    new Rol { Nombre = "Usuario" }
                );
                await context.SaveChangesAsync();
            }

            // =========================
            // TIPOS DE CLIENTE
            // IMPORTANTÍSIMO:
            // Tu registro busca "Mayoristas"/"Minoristas" (plural)
            // Así que sembramos en plural y normalizamos si existía en singular
            // =========================
            if (!await context.Tipo_Cliente.AnyAsync())
            {
                context.Tipo_Cliente.AddRange(
                    new Tipo_Cliente { Descripcion = "Mayoristas" },
                    new Tipo_Cliente { Descripcion = "Minoristas" }
                );
                await context.SaveChangesAsync();
            }
            else
            {
                // Normalización por si ya existen con singular:
                // Mayorista -> Mayoristas
                // Minorista -> Minoristas
                var mayor = await context.Tipo_Cliente.FirstOrDefaultAsync(t => t.Descripcion == "Mayorista");
                if (mayor != null) mayor.Descripcion = "Mayoristas";

                var minor = await context.Tipo_Cliente.FirstOrDefaultAsync(t => t.Descripcion == "Minorista");
                if (minor != null) minor.Descripcion = "Minoristas";

                await context.SaveChangesAsync();
            }

            // =========================
            // LISTAS DE PRECIO (singular)
            // =========================
            if (!await context.Lista_Precio.AnyAsync())
            {
                context.Lista_Precio.AddRange(
                    new Lista_Precio { Tipo = "Minorista" },
                    new Lista_Precio { Tipo = "Mayorista" }
                );
                await context.SaveChangesAsync();
            }

            // =========================
            // CATEGORÍAS DE PASTAS
            // =========================
            if (!await context.Categoria_Producto.AnyAsync())
            {
                context.Categoria_Producto.AddRange(
                    new Categoria_Producto { Nombre = "Pastas frescas" },
                    new Categoria_Producto { Nombre = "Pastas rellenas" },
                    new Categoria_Producto { Nombre = "Salsas" },
                    new Categoria_Producto { Nombre = "Combos" },
                    new Categoria_Producto { Nombre = "Congelados" }
                );

                await context.SaveChangesAsync();
            }

            // ============================================================
            // SEED CON IDs FIJOS para que coincida con el Razor /pedido
            // ============================================================

            await UpsertWithIdentityInsert(
                context,
                tableName: "Estado_Pedido",
                items: new[]
                {
                    new Estado_Pedido { Id = 1, Descripcion = "Pendiente" },
                    new Estado_Pedido { Id = 2, Descripcion = "En preparación" },
                    new Estado_Pedido { Id = 3, Descripcion = "Listo" },
                    new Estado_Pedido { Id = 4, Descripcion = "Entregado" },
                    new Estado_Pedido { Id = 5, Descripcion = "Cancelado" }
                },
                update: (db, incoming) => db.Descripcion = incoming.Descripcion
            );

            await UpsertWithIdentityInsert(
                context,
                tableName: "Forma_Pago",
                items: new[]
                {
                    new Forma_Pago { Id = 1, Metodo = "Efectivo" },
                    new Forma_Pago { Id = 2, Metodo = "Transferencia Bancaria" },
                    new Forma_Pago { Id = 3, Metodo = "Tarjeta" }
                },
                update: (db, incoming) => db.Metodo = incoming.Metodo
            );

            await UpsertWithIdentityInsert(
                context,
                tableName: "Metodo_Entrega",
                items: new[]
                {
                    new Metodo_Entrega { Id = 1, Descripcion = "Retiro en local" },
                    new Metodo_Entrega { Id = 2, Descripcion = "Envío a domicilio" }
                },
                update: (db, incoming) => db.Descripcion = incoming.Descripcion
            );

            // =========================
            // USUARIO ADMIN PRECARGADO (UPSERT)
            // (ACÁ es donde debe ir: en SeedAsync, NO dentro del helper)
            // =========================
            await UpsertAdmin(context);
        }

        private static async Task UpsertAdmin(Context context)
        {
            var rolAdminId = await context.Rol
                .Where(r => r.Nombre == "Administrador")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            // elegimos un tipo cliente válido para el admin (cualquiera sirve)
            var tipoClienteId = await context.Tipo_Cliente
                .Where(t => t.Descripcion == "Minoristas" || t.Descripcion == "Mayoristas" ||
                            t.Descripcion == "Minorista" || t.Descripcion == "Mayorista")
                .Select(t => t.Id)
                .FirstOrDefaultAsync();

            // elegimos una lista de precio válida
            var listaPrecioId = await context.Lista_Precio
                .Where(l => l.Tipo == "Minorista" || l.Tipo == "Mayorista")
                .Select(l => l.Id)
                .FirstOrDefaultAsync();

            if (rolAdminId == 0 || tipoClienteId == 0 || listaPrecioId == 0)
                return;

            var admin = await context.Usuario.FirstOrDefaultAsync(u => u.NombreUsuario == "admin");

            if (admin == null)
            {
                context.Usuario.Add(new Usuario
                {
                    Nombre = "Administrador",
                    Apellido = "Sistema",
                    Email = "admin@local",
                    NombreUsuario = "admin",
                    PasswordHash = "Admin123!",
                    Rol_Id = rolAdminId,
                    Tipo_Cliente_Id = tipoClienteId,
                    Lista_Precio_Id = listaPrecioId,
                    Telefono = "000000000",
                    Direccion = "Sistema"
                });
            }
            else
            {
                admin.Rol_Id = rolAdminId;
                admin.PasswordHash = "Admin123!";
                admin.Email = string.IsNullOrWhiteSpace(admin.Email) ? "admin@local" : admin.Email;

                if (admin.Tipo_Cliente_Id == 0) admin.Tipo_Cliente_Id = tipoClienteId;
                if (admin.Lista_Precio_Id == 0) admin.Lista_Precio_Id = listaPrecioId;
            }

            await context.SaveChangesAsync();
        }

        private static async Task UpsertWithIdentityInsert<TEntity>(
            Context context,
            string tableName,
            IEnumerable<TEntity> items,
            Action<TEntity, TEntity>? update = null)
            where TEntity : EntityBase
        {
            // 1) Actualiza si existe (por Id)
            foreach (var it in items)
            {
                var existente = await context.Set<TEntity>().FindAsync(it.Id);
                if (existente != null && update != null)
                {
                    update(existente, it);
                }
            }
            await context.SaveChangesAsync();

            // 2) Inserta los que faltan, con ID fijo (SQL Server)
            if (!context.Database.IsSqlServer())
            {
                foreach (var it in items)
                {
                    var existente = await context.Set<TEntity>().FindAsync(it.Id);
                    if (existente == null)
                        context.Set<TEntity>().Add(it);
                }
                await context.SaveChangesAsync();
                return;
            }

            await context.Database.OpenConnectionAsync();
            using var tx = await context.Database.BeginTransactionAsync();

            try
            {
                await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.{tableName} ON;");

                foreach (var it in items)
                {
                    var existente = await context.Set<TEntity>().FindAsync(it.Id);
                    if (existente == null)
                        context.Set<TEntity>().Add(it);
                }

                await context.SaveChangesAsync();

                await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.{tableName} OFF;");
                await tx.CommitAsync();
            }
            catch
            {
                try { await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.{tableName} OFF;"); }
                catch { /* ignore */ }

                await tx.RollbackAsync();
                throw;
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }
        }
    }
}
