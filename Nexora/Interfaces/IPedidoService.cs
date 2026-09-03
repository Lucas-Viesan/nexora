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
        public Task<PedidoResponse> AlterarQuantidadeItemPedido(int id, int itemPedido, ItemPedidoAlterarQuantidade itemPedidoQuantidadeDto);
        public Task<PedidoResponse> CancelarPedido(int id);
        public Task<PedidoResponse> RemoverItemPedido(int id, int itemId);
    }
}
