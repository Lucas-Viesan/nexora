using Nexora.DTOs.Produto;
using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IProdutoRepository
    {
        public Task CadastrarProduto(Produto produto);
        public Task<List<ProdutoResponse>> VerificarProdutosDisponiveis();
    }
}
