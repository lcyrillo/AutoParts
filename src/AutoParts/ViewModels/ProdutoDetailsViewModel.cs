namespace AutoParts.ViewModels
{
    public class ProdutoDetailsViewModel
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = "";

        public string Descricao { get; set; } = "";

        public string Categoria { get; set; } = "";

        public string Marca { get; set; } = "";

        public decimal PrecoVenda { get; set; }

        public int Estoque { get; set; }

        public bool Ativo { get; set; }
    }
}
