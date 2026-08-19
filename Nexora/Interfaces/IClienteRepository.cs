using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IClienteRepository
    {
        public Task<Cliente?> BuscarClientePorId(int id);
    }
}
