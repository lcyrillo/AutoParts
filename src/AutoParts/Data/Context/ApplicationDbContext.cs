using AutoParts.Models;
using AutoParts.Models.Produtos;
using Microsoft.EntityFrameworkCore;

namespace AutoParts.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Produto> Produtos => Set<Produto>();

    public DbSet<Categoria> Categorias => Set<Categoria>();

    public DbSet<Marca> Marcas => Set<Marca>();
}
