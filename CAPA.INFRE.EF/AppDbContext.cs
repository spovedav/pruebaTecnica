using CAPA.DOMAIN.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace CAPA.INFRE.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación Categoria-Producto
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria);
            
            // Configuración de la relación Producto-Transaccion
            modelBuilder.Entity<Transaccion>()
                .HasOne(t => t.Producto)
                .WithMany(p => p.Transacciones)
                .HasForeignKey(t => t.ProductoId);

            // Configuración de la propiedad calculada PrecioTotal
            modelBuilder.Entity<Transaccion>()
                .Property(t => t.PrecioTotal)
                .HasComputedColumnSql("[Cantidad] * [PrecioUnitario]");

            // Configuración de los valores por defecto
            modelBuilder.Entity<Producto>()
                .Property(p => p.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Producto>()
                .Property(p => p.Estado)
                .HasDefaultValue(true);

            modelBuilder.Entity<Transaccion>()
                .Property(t => t.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Transaccion>()
                .Property(t => t.Estado)
                .HasDefaultValue(true);
        }
    }
}
