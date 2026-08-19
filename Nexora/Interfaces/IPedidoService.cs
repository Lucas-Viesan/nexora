using Nexora.Dtos.Pedido;
using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IPedidoService
    {
        public Task<Pedido> CriarPedido(PedidoCreate dto, int? usuarioId);
    }
}
