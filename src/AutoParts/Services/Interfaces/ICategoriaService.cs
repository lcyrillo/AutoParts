using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoParts.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
