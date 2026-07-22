using System.ComponentModel.DataAnnotations;

namespace AutoParts.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o código")]
        public string Codigo { get; set; } = "";

        [Required(ErrorMessage = "Informe a descrição")]
        public string Descricao { get; set; } = "";

        [Display(Name = "Preço Compra")]
        public decimal PrecoCompra { get; set; }

        [Display(Name = "Preço Venda")]
        public decimal PrecoVenda { get; set; }
        public int Estoque { get; set; }

        [Display(Name = "Estoque mínimo")]
        public int EstoqueMinimo { get; set; }
        public int CategoriaId { get; set; }
        public int MarcaId { get; set; }
        public bool Ativo { get; set; }
    }
}
