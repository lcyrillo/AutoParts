using AutoParts.Models;

namespace AutoParts.Repositories.Interfaces;

public interface IMarcaRepository
{
    Task<IEnumerable<Marca>> GetAllAsync();
    Task<Marca?> GetByIdAsync(int id);
}
