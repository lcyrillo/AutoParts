using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.ViewModels;

public class ProdutoFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o código")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Display(Name = "Preço Compra")]
    public decimal PrecoCompra { get; set; }

    [Display(Name = "Preço Venda")]
    public decimal PrecoVenda { get; set; }

    public int Estoque { get; set; }

    [Display(Name = "Estoque Mínimo")]
    public int EstoqueMinimo { get; set; }

    [Required]
    public int? CategoriaId { get; set; }

    [Required]
    public int? MarcaId { get; set; }

    public bool Ativo { get; set; } = true;

    public string? Localizacao { get; set; }

    public string? Observacoes { get; set; }

    public IEnumerable<SelectListItem> Categorias { get; set; }
        = Enumerable.Empty<SelectListItem>();

    public IEnumerable<SelectListItem> Marcas { get; set; }
        = Enumerable.Empty<SelectListItem>();
}