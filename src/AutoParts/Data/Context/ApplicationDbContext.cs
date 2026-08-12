using AutoParts.Models;
using AutoParts.Models.Identity;
using AutoParts.Models.Produtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoParts.Data.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.Property(x => x.PrecoCompra)
                .HasPrecision(18, 2);

            entity.Property(x => x.PrecoVenda)
                .HasPrecision(18, 2);
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Produto> Produtos => Set<Produto>();

    public DbSet<Categoria> Categorias => Set<Categoria>();

    public DbSet<Marca> Marcas => Set<Marca>();
}
