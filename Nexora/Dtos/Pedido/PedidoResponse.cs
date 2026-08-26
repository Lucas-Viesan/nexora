using Nexora.Dtos.ItemPedido;

namespace Nexora.Dtos.Pedido
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Origem { get; set; } = string.Empty;

        public int? ClienteId { get; set; }
        public string? ClienteNome { get; set; }

        public int? CriadoPorUsuarioId { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public List<ItemPedidoResponse> Itens { get; set; } = new();
        public decimal Total { get; set; }
    }
}
