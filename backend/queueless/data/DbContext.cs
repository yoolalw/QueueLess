using Microsoft.EntityFrameworkCore;
using queueless.Models;

namespace queueless.Data
{
    public class QueuelessContext : DbContext
    {
        public QueuelessContext(DbContextOptions<QueuelessContext> options)
            : base(options)
        {
        }

        public DbSet<Usuarios> Users { get; set; }
        public DbSet<Produtos> Products { get; set; }
        public DbSet<Pedidos> Orders { get; set; }
        public DbSet<ItensPedidos> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USERS
            modelBuilder.Entity<Usuarios>()
                .ToTable("usuarios")
                .HasKey(u => u.Id);

            // PRODUCTS
            modelBuilder.Entity<Produtos>()
                .ToTable("produtos")
                .HasKey(p => p.Id);

            // ORDERS
            modelBuilder.Entity<Pedidos>()
                .ToTable("pedidos")
                .HasKey(o => o.Id);

            modelBuilder.Entity<Pedidos>()
                .HasOne<Usuarios>()
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ORDER ITEMS
            modelBuilder.Entity<ItensPedidos>()
                .ToTable("pedido_itens")
                .HasKey(oi => oi.Id);

            modelBuilder.Entity<ItensPedidos>()
                .HasOne<Pedidos>()
                .WithMany()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItensPedidos>()
                .HasOne<Produtos>()
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
