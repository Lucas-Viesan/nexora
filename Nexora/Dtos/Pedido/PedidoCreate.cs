using Nexora.Dtos.ItemPedido;

namespace Nexora.Dtos.Pedido
{
    public class PedidoCreate
    {
        public int? ClienteId { get; set; }
        public List<ItemPedidoCreate> Itens { get; set; }
    }
}
