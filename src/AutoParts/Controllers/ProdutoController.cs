using AutoParts.Services.Interfaces;
using AutoParts.ViewModels.Produto;
using Microsoft.AspNetCore.Mvc;

namespace AutoParts.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ILogger<ProdutoController> _logger;

        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IMarcaService _marcaService;

        public ProdutoController(
            ILogger<ProdutoController> logger,
            IProdutoService produtoService,
            ICategoriaService categoriaService,
            IMarcaService marcaService)
        {
            _logger = logger;
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            _marcaService = marcaService;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Tela de Produtos acessada.");

            var produtos = await _produtoService.GetAllAsync();

            _logger.LogInformation(
                "{Quantidade} produtos carregados.",
                produtos.Count());

            return View(produtos);
        }

        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("Tela de Cadastro de Produto acessada.");

            var vm = new ProdutoFormViewModel
            {
                Categorias = await _categoriaService.GetSelectListAsync(),
                Marcas = await _marcaService.GetSelectListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoFormViewModel model)
        {
            _logger.LogInformation(
                "Solicitação de cadastro recebida. Código={Codigo}",
                model.Codigo);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(
                    "ModelState inválido para o produto {Codigo}.",
                    model.Codigo);

                model.Categorias = await _categoriaService.GetSelectListAsync();
                model.Marcas = await _marcaService.GetSelectListAsync();

                return View(model);
            }

            try
            {
                await _produtoService.CriarAsync(model);

                _logger.LogInformation(
                    "Cadastro concluído. Código={Codigo}",
                    model.Codigo);

                TempData["Sucesso"] = "Produto cadastrado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao cadastrar o produto {Codigo}.",
                    model.Codigo);

                ModelState.AddModelError("", ex.Message);

                model.Categorias = await _categoriaService.GetSelectListAsync();
                model.Marcas = await _marcaService.GetSelectListAsync();

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation(
                "Tela de edição de produto acessada. Id={id}",
                id);

            var produto = await _produtoService.GetByIdAsync(id);

            if (produto == null)
            {
                _logger.LogWarning(
                    "Produto Id={id} não encontrado para edição.",
                    id);

                return NotFound();
            }

            var model = new ProdutoFormViewModel
            {
                Id = produto.Id,
                Codigo = produto.Codigo,
                Descricao = produto.Descricao,
                PrecoCompra = produto.PrecoCompra,
                PrecoVenda = produto.PrecoVenda,
                Estoque = produto.Estoque,
                EstoqueMinimo = produto.EstoqueMinimo,
                CategoriaId = produto.CategoriaId,
                MarcaId = produto.MarcaId,
                Ativo = produto.Ativo,
                Localizacao = produto.Localizacao,
                Observacoes = produto.Observacoes,

                Categorias = await _categoriaService.GetSelectListAsync(),
                Marcas = await _marcaService.GetSelectListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            ProdutoFormViewModel model)
        {
            _logger.LogInformation(
                "Solicitação de atualização recebida. Id={Id}, Código={Codigo}",
                id,
                model.Codigo);

            if (id != model.Id)
            {
                _logger.LogWarning(
                    "Id da URL diferente do Id do formulário. URL={UrlId}, Form={FormId}",
                    id,
                    model.Id);

                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(
                    "ModelState inválido para atualização do produto Id={Id}",
                    id);

                model.Categorias =
                    await _categoriaService.GetSelectListAsync();

                model.Marcas =
                    await _marcaService.GetSelectListAsync();

                return View(model);
            }

            try
            {
                await _produtoService.AtualizarAsync(id, model);

                _logger.LogInformation(
                    "Atualização concluída. Id={Id}, Código={Codigo}",
                    id,
                    model.Codigo);

                TempData["Sucesso"] =
                    "Produto atualizado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao atualizar produto Id={Id}, Código={Codigo}.",
                    id,
                    model.Codigo);

                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                model.Categorias =
                    await _categoriaService.GetSelectListAsync();

                model.Marcas =
                    await _marcaService.GetSelectListAsync();

                return View(model);
            }
        }
    }
}
