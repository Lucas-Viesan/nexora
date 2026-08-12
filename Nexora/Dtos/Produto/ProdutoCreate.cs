using Nexora.Entities;
using System.ComponentModel.DataAnnotations;

namespace Nexora.DTOs.Produto
{
    public class ProdutoCreate
    {
            public string Nome { get; set; } = string.Empty;

            public string? Descricao { get; set; }

            public decimal Preco { get; set; }

    }
}
