using AutoParts.Data.Context;
using AutoParts.Models;
using AutoParts.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoParts.Repositories.Implementations
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly ApplicationDbContext _context;

        public MarcaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Marca>> GetAllAsync()
        {
            return await _context.Marcas
                .OrderBy(c => c.Descricao)
                .ToListAsync();
        }

        public async Task<Marca?> GetByIdAsync(int id)
        {
            return await _context.Marcas
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
