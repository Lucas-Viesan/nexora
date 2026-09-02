using Nexora.Dtos.ItemPedido;
using Nexora.Dtos.Pedido;
using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IPedidoService
    {
        public Task<Pedido> CriarPedido(PedidoCreate dto, int? usuarioId);
        public Task<PedidoResponse> ConsultarPedidoId(int id);
        public Task<PedidoResponse> AdicionarItemAoPedido(int id, ItemPedidoCreate itemPedidoDto);
    }
}
