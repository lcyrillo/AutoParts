using AutoParts.Models.Produtos;
using AutoParts.ViewModels.Produto;

namespace AutoParts.Services.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<Produto?> GetByIdAsync(int id);
    Task CriarAsync(ProdutoFormViewModel model);
    Task AtualizarAsync(int id, ProdutoFormViewModel model);
    Task ExcluirAsync(int id);
}