using Nexora.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Entities
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public StatusPedido Status { get; set; }

        [Required]
        public OrigemPedido Origem { get; set; }

        public int? ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }

        public int? CriadoPorUsuarioId { get; set; }

        [ForeignKey(nameof(CriadoPorUsuarioId))]
        public Usuario? CriadoPorUsuario { get; set; }

        public DateTime DataCriacao { get; private set; }

        public DateTime? DataAtualizacao { get; set; }

        public List<ItemPedido> Itens { get; set; } 

        public Pagamento? Pagamento { get; set; }
    }
}
