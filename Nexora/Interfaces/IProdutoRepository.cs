using Nexora.DTOs.Produto;
using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IProdutoRepository
    {
        public async Task CadastrarProduto(Produto produto);
    }
}
