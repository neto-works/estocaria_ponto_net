using EstocariaNet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EstocariaNet.Shared.Context
{
    public class AppDbContext : IdentityDbContext<AplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Estoque>? Estoques { get; set; }
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Estoquista>? Estoquistas { get; set; }
        public DbSet<Lancamento>? Lancamentos { get; set; }
        public DbSet<Relatorio>? Relatorios { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configuração adicional de chaves estrangeiras, índices, etc.
            builder.Entity<Estoquista>().HasOne(e => e.AplicationUser).WithMany().HasForeignKey(e => e.AplicationUserEstoquistaId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Estoquista>().HasOne(e => e.Estoque).WithMany().HasForeignKey(e => e.EstoquistaEstoqueId).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Admin>().HasOne(a => a.AplicationUser).WithMany().HasForeignKey(a => a.AplicationUserAdminId).OnDelete(DeleteBehavior.Cascade);
        }

    }
}
