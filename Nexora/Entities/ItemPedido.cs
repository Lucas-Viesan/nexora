using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Entities
{
    [Table("ItensPedido")]
    public class ItemPedido
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public int PedidoId { get; set; }

        [ForeignKey(nameof(PedidoId))]
        public Pedido Pedido { get; set; } 

        [Required]
        public int ProdutoId { get; set; }

        [ForeignKey(nameof(ProdutoId))]
        public Produto Produto { get; set; } 

        [Required]
        [MaxLength(150)]
        public string NomeProduto { get; set; } 

        [Required]
        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoUnitario { get; set; }

        [MaxLength(300)]
        public string? Observacao { get; set; }

        public decimal Subtotal => Quantidade * PrecoUnitario;

        private ItemPedido() { }

        public ItemPedido(Produto produto, int quantidade, string? observacao)
        {
            if (produto == null)
                throw new ArgumentException("Produto é obrigatório.");
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");


            ProdutoId = produto.Id;
            Produto = produto;
            NomeProduto = produto.Nome;
            Quantidade = quantidade;
            PrecoUnitario = produto.Preco;
            Observacao = observacao;
            
        }

        public void AlterarQuantidade(int novaQuantidade)
        {
            if(novaQuantidade <= 0){
                throw new ArgumentException("Quantidade deve ser maior que zero.");
            }
            Quantidade = novaQuantidade;
        }

        public void AlterarObservacao(string? observacao)
        {
            Observacao = observacao;
        }


    }

    
}
