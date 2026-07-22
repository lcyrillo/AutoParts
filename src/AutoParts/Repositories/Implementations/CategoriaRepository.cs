using AutoParts.Data.Context;
using AutoParts.Models;
using AutoParts.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoParts.Repositories.Implementations
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _context.Categorias
                .OrderBy(c => c.Descricao)
                .ToListAsync();
        }

        public async Task<Categoria?> GetByIdAsync(int id)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
