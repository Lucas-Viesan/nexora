using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IProdutoRepository
    {
        public Task CadastrarProduto(Produto produto);
    }
}
