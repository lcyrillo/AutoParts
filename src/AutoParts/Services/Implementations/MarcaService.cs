using AutoParts.Repositories.Interfaces;
using AutoParts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoParts.Services.Implementations
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _repository;

        public MarcaService(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAsync()
        {
            var marcas = await _repository.GetAllAsync();

            return marcas.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Descricao
            });
        }
    }
}
