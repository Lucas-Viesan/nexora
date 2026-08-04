using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Entities
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; } 

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Telefone { get; set; }

        public bool Ativo { get; set; } 

        public DateTime DataCriacao { get; private set; } 

        public List<Pedido> Pedidos { get; set; } 
    }
}
