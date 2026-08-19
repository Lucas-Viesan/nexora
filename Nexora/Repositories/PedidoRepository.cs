using Nexora.Data.Context;
using Nexora.Entities;
using Nexora.Interfaces;

namespace Nexora.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CadastrarPedido(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task SalvarAlteracoes()
        {
            await _context.SaveChangesAsync();
        }
    }
}
