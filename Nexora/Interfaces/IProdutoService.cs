using Nexora.Dtos.Produto;
using Nexora.DTOs.Produto;
using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IProdutoService
    {
        public Task<ProdutoResponse> CadastrarProduto(int usuarioId, ProdutoCreate produtoDto);
        public  Task<List<ProdutoResponse?>> BuscarTodosProdutosDisponiveis();
        public Task<ProdutoResponse?> AlterarInfoDosProdutos(int usuarioId, int produtoId, ProdutoAlteracaoDados produtoAlteracao);
        public Task<ProdutoResponse?> DesativarProduto(int produtoId, int usuarioId);
        public Task<ProdutoResponse?> ReativarProduto(int produtoId, int usuarioId);
        public Task<ProdutoResponse?> BuscarProdutoPorId(int produtoId);

    }
}
