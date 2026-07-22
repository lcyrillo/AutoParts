using AutoParts.Data.Context;
using AutoParts.Models.Produtos;
using AutoParts.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoParts.Repositories.Implementations
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsCodigoAsync(string codigo)
        {
            return await _context.Produtos
                .AnyAsync(x => x.Codigo == codigo);
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _context.Produtos
                   .Include(x => x.Categoria)
                   .Include(x => x.Marca)
                   .ToListAsync();
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
            return await _context.Produtos
                    .Include(x => x.Categoria)
                    .Include(x => x.Marca)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }
    }
}
