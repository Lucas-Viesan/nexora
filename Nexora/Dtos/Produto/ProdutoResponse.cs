namespace Nexora.DTOs.Produto
{
    public class ProdutoResponse
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public decimal Preco { get; set; }

        public bool Disponivel { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
