using AutoParts.Models;

namespace AutoParts.Models.Produtos
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Codigo { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public decimal PrecoCompra { get; private set; }
        public decimal PrecoVenda { get; private set; }
        public int Estoque { get; private set; }
        public int EstoqueMinimo { get; private set; }
        public int CategoriaId { get; private set; }
        public Categoria Categoria { get; set; } = null!;
        public int MarcaId { get; set; }
        public Marca Marca { get; set; } = null!;
        public bool Ativo { get; private set; }
        public string? Localizacao { get; set; }
        public string? Observacoes { get; set; }
        // Necessário para o Entity Framework
        private Produto()
        {
        }

        public Produto(
            string codigo,
            string descricao,
            decimal precoCompra,
            decimal precoVenda,
            int estoque,
            int estoqueMinimo,
            int categoriaId,
            int marcaId,
            bool ativo,
            string? localizacao,
            string? observacoes)
        {
            Codigo = codigo;
            Descricao = descricao;
            PrecoCompra = precoCompra;
            PrecoVenda = precoVenda;
            Estoque = estoque;
            EstoqueMinimo = estoqueMinimo;
            CategoriaId = categoriaId;
            MarcaId = marcaId;
            Ativo = ativo;
            Localizacao = localizacao;
            Observacoes = observacoes;
        }

        public void Atualizar(
        string codigo,
        string descricao,
        decimal precoCompra,
        decimal precoVenda,
        int estoque,
        int estoqueMinimo,
        int categoriaId,
        int marcaId,
        bool ativo,
        string? localizacao,
        string? observacoes)
    {
        Codigo = codigo;
        Descricao = descricao;
        PrecoCompra = precoCompra;
        PrecoVenda = precoVenda;
        Estoque = estoque;
        EstoqueMinimo = estoqueMinimo;
        CategoriaId = categoriaId;
        MarcaId = marcaId;
        Ativo = ativo;
        Localizacao = localizacao;
        Observacoes = observacoes;
    }

    }
}
