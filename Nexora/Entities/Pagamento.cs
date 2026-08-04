using Nexora.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Entities
{
    [Table("Pagamentos")]
    public class Pagamento
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public int PedidoId { get; set; }

        [ForeignKey(nameof(PedidoId))]
        public Pedido Pedido { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Required]
        public FormaPagamento FormaPagamento { get; set; }

        [Required]
        public StatusPagamento Status { get; set; }

        public DateTime DataCriacao { get; private set; } 

        public DateTime? DataConfirmacao { get; set; }
    }
}
