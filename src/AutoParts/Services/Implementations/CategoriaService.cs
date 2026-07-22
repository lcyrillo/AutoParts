using AutoParts.Repositories.Interfaces;
using AutoParts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoParts.Services.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var categorias = await _repository.GetAllAsync();

            return categorias.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Descricao
            });
        }
    }
}
