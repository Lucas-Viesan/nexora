using Microsoft.EntityFrameworkCore;
using Nexora.Data.Context;
using Nexora.Entities;
using Nexora.Interfaces;

namespace Nexora.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<Usuario?> BuscarPorId(int id)
        {
         return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        }
    }
}
