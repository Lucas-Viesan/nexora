using Microsoft.EntityFrameworkCore;
using Nexora.Data.Context;
using Nexora.Entities;
using Nexora.Interfaces;

namespace Nexora.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cliente?> BuscarClientePorId(int id)
        {
            var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null)
            {
                return null;
            }
            return cliente;
        }
    }
}
