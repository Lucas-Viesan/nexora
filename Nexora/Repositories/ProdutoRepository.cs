using Nexora.Data.Context;
using Nexora.DTOs.Produto;
using Nexora.Entities;
using Nexora.Interfaces;

namespace Nexora.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context) 
        { 
            _context = context;
        }
        public async Task CadastrarProduto(Produto produto)
        {
          await _context.Produtos.AddAsync(produto);
          await _context.SaveChangesAsync();
        }
    }
}
