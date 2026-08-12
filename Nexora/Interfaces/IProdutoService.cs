using Nexora.DTOs.Produto;
using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IProdutoService
    {
        public Task<ProdutoResponse> CadastrarProduto(ProdutoCreate produtoDto, int usuarioId);
        public  Task<List<ProdutoResponse>> BuscarTodosProdutosDisponiveis();
    }
}
