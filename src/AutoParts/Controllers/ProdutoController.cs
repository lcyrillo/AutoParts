using AutoParts.Services.Interfaces;
using AutoParts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoParts.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IMarcaService _marcaService;

        public ProdutoController(
            IProdutoService produtoService,
            ICategoriaService categoriaService,
            IMarcaService marcaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            _marcaService = marcaService;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoService.GetAllAsync();

            return View(produtos);
        }

        public async Task<IActionResult> Create()
        {
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
            if (!ModelState.IsValid)
            {
                model.Categorias = await _categoriaService.GetSelectListAsync();
                model.Marcas = await _marcaService.GetSelectListAsync();

                return View(model);
            }

            try
            {
                await _produtoService.CriarAsync(model);

                TempData["Sucesso"] = "Produto cadastrado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                model.Categorias = await _categoriaService.GetSelectListAsync();
                model.Marcas = await _marcaService.GetSelectListAsync();

                return View(model);
            }
        }
    }
}
