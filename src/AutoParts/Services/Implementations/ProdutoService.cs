using AutoParts.Models.Produtos;
using AutoParts.Repositories.Interfaces;
using AutoParts.Services.Interfaces;
using AutoParts.ViewModels;

namespace AutoParts.Services.Implementations
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }


        public async Task<Produto?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }


        public async Task CriarAsync(ProdutoFormViewModel model)
        {
            if (await _repository.ExistsCodigoAsync(model.Codigo))
                throw new Exception("Já existe um produto com esse código");

            var produto = new Produto(
                model.Codigo,
                model.Descricao,
                model.PrecoCompra,
                model.PrecoVenda,
                model.Estoque,
                model.EstoqueMinimo,
                model.CategoriaId!.Value,
                model.MarcaId!.Value,
                model.Ativo,
                model.Localizacao,
                model.Observacoes);

            await _repository.AddAsync(produto);
        }


        public async Task AtualizarAsync(Produto produto)
        {
            await _repository.UpdateAsync(produto);
        }


        public async Task ExcluirAsync(Produto produto)
        {
            await _repository.DeleteAsync(produto);
        }
    }
}
