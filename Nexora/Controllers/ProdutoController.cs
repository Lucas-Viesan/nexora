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

        [HttpGet("{produtoId}")]
        public async Task<ActionResult<ProdutoResponse>> BuscarProdutoPorId(int produtoId)
        {
            var produto = await _service.BuscarProdutoPorId(produtoId);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
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


        [HttpPut]
        [Route("produto/{produtoId}/desativar")]
        public async Task<ActionResult<ProdutoResponse>> DesativarProduto(int produtoId, int usuarioId)
        {
            var produtoDesativado = await _service.DesativarProduto(produtoId, usuarioId);
            if (produtoDesativado == null)
            {
                return NotFound();
            }
            return Ok(produtoDesativado);
        }


        [HttpPut]
        [Route("produto/{produtoId}/reativar")]
        public async Task<ActionResult<ProdutoResponse>> ReativarProduto(int produtoId, int usuarioId)
        {
            var produtoReativado = await _service.ReativarProduto(produtoId, usuarioId);
            if (produtoReativado == null)
            {
                return NotFound();
            }
            return Ok(produtoReativado);
        }


    }
}
