using AutoParts.Models.Produtos;
using AutoParts.ViewModels;

namespace AutoParts.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto?> GetByIdAsync(int id);
        Task CriarAsync(ProdutoFormViewModel produto);
        Task AtualizarAsync(Produto produto);
        Task ExcluirAsync(Produto produto);
    }
}
