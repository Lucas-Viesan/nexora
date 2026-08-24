using Microsoft.AspNetCore.Mvc;
using Nexora.Dtos.Pedido;
using Nexora.Entities;
using Nexora.Interfaces;

namespace Nexora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        public PedidoController(IPedidoService pedidoService) 
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        [Route("pedido")]
        public async Task<ActionResult<Pedido>> CadastrarNovoPedido([FromBody]PedidoCreate pedidoDto, [FromQuery] int? usuarioId)
        {
            var pedido = await _pedidoService.CriarPedido(pedidoDto, usuarioId);
            return Created();
        }
    }
}
