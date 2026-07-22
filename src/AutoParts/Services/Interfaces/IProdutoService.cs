using AutoParts.Models.Produtos;

namespace AutoParts.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto?> GetByIdAsync(int id);
        Task CriarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task ExcluirAsync(Produto produto);
    }
}
