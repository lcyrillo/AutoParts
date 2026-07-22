using AutoParts.Models.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoParts.Data.Mappings
{
    public class ProdutoMap
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Codigo)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.PrecoCompra)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.PrecoVenda)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Categoria)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.CategoriaId);

            builder.HasOne(x => x.Marca)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.MarcaId);
        }
    }
}
