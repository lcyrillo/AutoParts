using AutoParts.Services.Interfaces;
using AutoParts.ViewModels;
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
    }
}
