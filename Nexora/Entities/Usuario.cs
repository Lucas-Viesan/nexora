using Nexora.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Entities
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; } 

        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        [MaxLength(500)]
        public string SenhaHash { get; set; } 

        public PerfilUsuario Perfil { get; set; }

        public bool Ativo { get; set; } 
        public DateTime DataCriacao { get; private set; } 

        public List<Pedido> PedidosCriados { get; set; } 
    }
}
