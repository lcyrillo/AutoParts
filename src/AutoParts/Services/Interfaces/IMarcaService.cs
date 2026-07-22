using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoParts.Services.Interfaces
{
    public interface IMarcaService
    {
        Task<IEnumerable<SelectListItem>> GetSelectListAsync();
    }
}
