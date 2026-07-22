using AutoParts.Data.Context;
using AutoParts.Models;

namespace AutoParts.Data.Seed;

public class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        //Garante que banco esteja criado
        await context.Database.EnsureCreatedAsync();

        // ==========================
        // Categorias
        // ==========================

        if (!context.Categorias.Any())
        {
            context.Categorias.AddRange(

                new Categoria("Freios"),
                new Categoria("Motor"),
                new Categoria("Suspensão"),
                new Categoria("Filtros"),
                new Categoria("Elétrica"),
                new Categoria("Lubrificantes"),
                new Categoria("Escapamento"),
                new Categoria("Transmissão")

            );
        }

        // ==========================
        // Marcas
        // ==========================

        if (!context.Marcas.Any())
        {
            context.Marcas.AddRange(

                new Marca("Bosch"),
                new Marca("NGK"),
                new Marca("SKF"),
                new Marca("TRW"),
                new Marca("Cofap"),
                new Marca("Valeo"),
                new Marca("Mahle"),
                new Marca("Mann Filter")

            );
        }

        await context.SaveChangesAsync();

    }
}
