using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<Usuario?> BuscarPorId(int id);
    }
}
