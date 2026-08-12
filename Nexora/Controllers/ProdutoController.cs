using Microsoft.AspNetCore.Mvc;
using Nexora.DTOs.Produto;
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
        public async Task<ActionResult<ProdutoResponse>> CadastrarNovoProduto([FromBody] ProdutoCreate produto, int usuarioId)
        {
            var respostaDto = await _service.CadastrarProduto(produto, usuarioId);
            return Created();
        }
    }
}
