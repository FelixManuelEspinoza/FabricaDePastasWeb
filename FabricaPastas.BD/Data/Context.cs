using FabricaPastas.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FabricaPastas.BD.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        #region DbSets

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Tipo_Cliente> Tipo_Cliente { get; set; }

        public DbSet<Producto> Producto { get; set; }
        public DbSet<Categoria_Producto> Categoria_Producto { get; set; }

        public DbSet<Lista_Precio> Lista_Precio { get; set; }
        public DbSet<Detalle_Lista_Precio> Detalle_Lista_Precio { get; set; }

        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Detalle_Pedido> Detalle_Pedido { get; set; }
        public DbSet<Estado_Pedido> Estado_Pedido { get; set; }
        public DbSet<Forma_Pago> Forma_Pago { get; set; }
        public DbSet<Metodo_Entrega> Metodo_Entrega { get; set; }

        public DbSet<Promocion> Promocion { get; set; }
        public DbSet<Promocion_Producto> Promocion_Producto { get; set; }

        // ✅ NUEVO
        public DbSet<Promocion_Rango> Promocion_Rango { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔒 Evitar borrados en cascada
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            #region Relaciones explícitas

            modelBuilder.Entity<Detalle_Pedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(dp => dp.Pedido_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Detalle_Lista_Precio>()
                .HasOne(dlp => dlp.Lista_Precio)
                .WithMany(lp => lp.Detalles)
                .HasForeignKey(dlp => dlp.Lista_Precio_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ Pedido -> Usuario/Estado/Entrega/Pago
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.Usuario_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Estado_Pedido)
                .WithMany()
                .HasForeignKey(p => p.Estado_Pedido_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Metodo_Entrega)
                .WithMany()
                .HasForeignKey(p => p.Metodo_Entrega_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Forma_Pago)
                .WithMany()
                .HasForeignKey(p => p.Forma_Pago_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ Promocion_Rango -> Promocion / Tipo_Cliente
            modelBuilder.Entity<Promocion_Rango>()
                .HasOne(r => r.Promocion)
                .WithMany(p => p.Rangos)
                .HasForeignKey(r => r.Promocion_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Promocion_Rango>()
                .HasOne(r => r.Tipo_Cliente)
                .WithMany()
                .HasForeignKey(r => r.Tipo_Cliente_Id)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Precisión decimal

            modelBuilder.Entity<Detalle_Pedido>()
                .Property(p => p.Precio_Unitario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Detalle_Pedido>()
                .Property(p => p.Subtotal)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioBase)
                .HasColumnType("decimal(18,2)");

            // ✅ NUEVO
            modelBuilder.Entity<Promocion_Rango>()
                .Property(p => p.Descuento_Porcentaje)
                .HasColumnType("decimal(18,2)");

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
