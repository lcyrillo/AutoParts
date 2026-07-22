using AutoParts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoParts.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _service;
        public ProdutoController(
            IProdutoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _service.GetAllAsync();

            return View(produtos);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
