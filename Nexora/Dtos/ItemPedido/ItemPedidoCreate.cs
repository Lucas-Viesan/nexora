namespace Nexora.Dtos.ItemPedido
{
    public class ItemPedidoCreate
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public string? Observacao { get; set; }
    }
}
