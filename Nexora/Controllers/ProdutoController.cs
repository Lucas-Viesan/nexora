using Microsoft.AspNetCore.Mvc;
using Nexora.Dtos.Produto;
using Nexora.DTOs.Produto;
using Nexora.Entities;
using Nexora.Interfaces;

namespace Nexora.Controllers
{
    public class ProdutoController : Controller
    {
        private IProdutoService _service;
        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("produto")]
        public async Task<ActionResult<ProdutoResponse>> CadastrarNovoProduto(int usuarioId, [FromBody] ProdutoCreate produto)
        {
            var respostaDto = await _service.CadastrarProduto(usuarioId, produto);
            return Created();
        }

        [HttpGet]
        [Route("produto")]
        public async Task<List<ProdutoResponse>> BuscarProdutosDisponiveis()
        { 
            var respostaDto = await _service.BuscarTodosProdutosDisponiveis();
            return respostaDto;
        }

        [HttpPut]
        [Route("produto/{produtoId}")]
        public async Task<ActionResult<ProdutoResponse>> AlterarDadosProduto(int produtoId, int usuarioId, [FromBody] ProdutoAlteracaoDados produtoAlteracao)
        {
            var produtoAlterado = await _service.AlterarInfoDosProdutos(produtoId, usuarioId, produtoAlteracao);
            if(produtoAlterado == null)
            {
                return NotFound();
            }
            return Ok(produtoAlterado);
        }
    }
}
