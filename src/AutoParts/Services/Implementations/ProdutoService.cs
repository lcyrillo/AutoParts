using AutoParts.Models.Produtos;
using AutoParts.Repositories.Interfaces;
using AutoParts.Services.Interfaces;

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


        public async Task CriarAsync(Produto produto)
        {

            if (string.IsNullOrWhiteSpace(produto.Codigo))
                throw new Exception("Código obrigatório");


            if (string.IsNullOrWhiteSpace(produto.Descricao))
                throw new Exception("Descrição obrigatória");


            if (produto.PrecoVenda <= 0)
                throw new Exception("Preço de venda inválido");


            if (produto.PrecoVenda < produto.PrecoCompra)
                throw new Exception(
                    "Preço de venda não pode ser menor que o custo");


            if (await _repository.ExistsCodigoAsync(produto.Codigo))
                throw new Exception(
                    "Já existe produto com esse código");


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
