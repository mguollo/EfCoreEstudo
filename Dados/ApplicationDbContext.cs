using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;

namespace Dados
{   
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("Produto");
            modelBuilder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(50);
            
            modelBuilder.Entity<Pedido>().HasKey(p => p.Numero);
            modelBuilder.Entity<Pedido>().Property(p => p.Data)
                .IsRequired()
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Produto> Produtos { get; set; }      

        public DbSet<Pedido> Pedido { get; set; }
        
    }
} 