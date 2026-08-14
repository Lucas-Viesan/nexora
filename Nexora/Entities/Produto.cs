using Nexora.Dtos.Produto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Entities
{
    public class Produto
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        [MaxLength(500)]
        public string? Descricao { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        public bool Disponivel { get; set; }

        public DateTime DataCriacao { get; private set; } 

        public DateTime? DataAtualizacao { get; set; }

        public List<ItemPedido> ItensPedido { get; set; }

        private Produto()
        {
        }

        public Produto(string nome, string? descricao, decimal preco)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do produto é obrigatório.");

            if (preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero.");
            
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Disponivel = true;
            DataCriacao = DateTime.UtcNow;
        }

        public void Alterar(ProdutoAlteracaoDados dados)
        {
            if (string.IsNullOrWhiteSpace(dados.Nome))
                throw new ArgumentException("O nome do produto é obrigatório.");

            if (dados.Preco <= 0)
                throw new ArgumentException("O preço do produto deve ser maior que zero.");

            Nome = dados.Nome;
            Descricao = dados.Descricao;
            Preco = dados.Preco;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Desativar()
        {
            if (!Disponivel)
                return;

            Disponivel = false;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Ativar()
        {
            if (Disponivel)
                return;

            Disponivel = true;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
    
}
