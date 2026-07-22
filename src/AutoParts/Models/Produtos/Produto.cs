using AutoParts.Models;

namespace AutoParts.Models.Produtos
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Codigo { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public double PrecoCompra { get; private set; }
        public double PrecoVenda { get; private set; }
        public int Estoque { get; private set; }
        public int EstoqueMinimo { get; private set; }
        public int CategoriaId { get; private set; }
        public Categoria Categoria { get; set; } = null!;
        public int MarcaId { get; set; }
        public Marca Marca { get; set; } = null!;
        public bool Ativo { get; private set; }
        //public string? Localizacao { get; set; }
        //public string? Observacoes { get; set; }
    }
}
