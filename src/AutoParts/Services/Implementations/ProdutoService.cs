using AutoParts.Models.Produtos;
using AutoParts.Repositories.Interfaces;
using AutoParts.Services.Interfaces;
using AutoParts.ViewModels.Produto;
using AutoParts.Services.Exceptions;

namespace AutoParts.Services.Implementations
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly ILogger<ProdutoService> _logger;

        public ProdutoService(
            IProdutoRepository repository, 
            ILogger<ProdutoService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            _logger.LogInformation("Consultando produtos.");

            var produtos = await _repository.GetAllAsync();

            _logger.LogInformation(
                "Consulta realizada. {Quantidade} produtos encontrados.",
                produtos.Count());

            return produtos;
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
            _logger.LogInformation(
                "Consultando produto Id={Id}.",
                id);

            var produto = await _repository.GetByIdAsync(id);

            if (produto == null)
            {
                _logger.LogWarning(
                    "Produto Id={Id} não encontrado.",
                    id);
            }

            return produto;
        }

        public async Task CriarAsync(ProdutoFormViewModel model)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando regra de negócio para cadastro do produto {Codigo}.",
                    model.Codigo);

                await ValidarProdutoAsync(model);

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

                _logger.LogInformation(
                    "Produto criado com sucesso. Id={Id}, Código={Codigo}.",
                    produto.Id,
                    produto.Codigo);
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Regra de negócio violada ao cadastrar o produto {Codigo}.",
                    model.Codigo);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro durante a regra de negócio de cadastro do produto {Codigo}.",
                    model.Codigo);

                throw;
            }
        }

        public async Task AtualizarAsync(
            int id,
            ProdutoFormViewModel model)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando atualização do produto Id={Id}, Código={Codigo}.",
                    id,
                    model.Codigo);

                var produto = await _repository.GetByIdAsync(id);

                if (produto == null)
                {
                    _logger.LogWarning(
                        "Produto Id={Id} não encontrado para atualização.",
                        id);

                    throw new Exception("Produto não encontrado.");
                }

                await ValidarProdutoAsync(model, id);

                produto.Atualizar(
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

                await _repository.UpdateAsync(produto);

                _logger.LogInformation(
                    "Produto atualizado com sucesso. Id={Id}, Código={Codigo}.",
                    produto.Id,
                    produto.Codigo);
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Regra de negócio violada durante atualização do produto Id={Id}.",
                    id);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro durante atualização do produto Id={Id}.",
                    id);

                throw;
            }
        }
        public async Task ExcluirAsync(int id)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando exclusão do produto Id={Id}.",
                    id);

                var produto = await _repository.GetByIdAsync(id);

                if (produto == null)
                {
                    _logger.LogWarning(
                        "Produto Id={Id} não encontrado para exclusão.",
                        id);

                    throw new BusinessException("Produto não encontrado.");
                }

                await _repository.DeleteAsync(produto);

                _logger.LogInformation(
                    "Produto excluído com sucesso. Id={Id}, Código={Codigo}.",
                    produto.Id,
                    produto.Codigo);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(
                    ex,
                    "Regra de negócio violada ao excluir o produto Id={Id}.",
                    id);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro inesperado ao excluir o produto Id={Id}.",
                    id);

                throw;
            }
        }

        private async Task ValidarProdutoAsync(
            ProdutoFormViewModel model,
            int? id = null)
        {
            if (string.IsNullOrEmpty(model.Codigo))
                throw new Exception("O código do produto é obrigatório.");

            if (string.IsNullOrWhiteSpace(model.Descricao))
                throw new BusinessException("A descrição do produto é obrigatória.");

            if (model.PrecoVenda <= 0)
                throw new BusinessException(
                    "O preço de venda deve ser maior que zero.");

            if (model.PrecoVenda < model.PrecoCompra)
                throw new BusinessException(
                    "O preço de venda não pode ser menor que o preço de compra.");

            if (!model.CategoriaId.HasValue)
                throw new BusinessException(
                    "A categoria do produto é obrigatória.");

            if (!model.MarcaId.HasValue)
                throw new BusinessException(
                    "A marca do produto é obrigatória.");

            var codigoExiste = await _repository.ExistsCodigoAsync(model.Codigo);

            if (codigoExiste)
            {
                if (!id.HasValue)
                {
                    throw new BusinessException(
                        "Já existe um produto com esse código.");
                }

                var produtoExistente = await _repository.GetByIdAsync(id.Value);

                if (produtoExistente == null)
                {
                    throw new BusinessException(
                        "Produto não encontrado.");
                }

                if (!string.Equals(
                        produtoExistente.Codigo,
                        model.Codigo,
                        StringComparison.OrdinalIgnoreCase))
                {
                    throw new BusinessException(
                        "Já existe um produto com esse código.");
                }
            }
        }
    }
}
