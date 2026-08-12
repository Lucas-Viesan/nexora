using Microsoft.EntityFrameworkCore;
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

        public async Task<List<ProdutoResponse>> VerificarProdutosDisponiveis()
        {
            var produtosDisponiveis = await _context.Produtos
                .Where(p => p.Disponivel == true)
                .Select(p => new ProdutoResponse
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    Preco = p.Preco,
                    Disponivel = p.Disponivel
                })
                .ToListAsync();

            return produtosDisponiveis;
        }
    }
}
