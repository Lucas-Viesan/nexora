using Nexora.Dtos.Produto;
using Nexora.DTOs.Produto;
using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IProdutoRepository
    {
        public Task CadastrarProduto(Produto produto);
        public Task<List<ProdutoResponse>> VerificarProdutosDisponiveis();
        public Task<Produto?> AlterarInfoProduto(int id, ProdutoAlteracaoDados produtoAlteracao);
        public Task<Produto?> BuscarProdutoPorId(int id);
        public Task SalvarAlteracoes();
    }
}
