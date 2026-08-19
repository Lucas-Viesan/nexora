using Microsoft.AspNetCore.Mvc;
using Nexora.Dtos.Pedido;
using Nexora.Entities;
using Nexora.Interfaces;

namespace Nexora.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;
        public PedidoController(IPedidoService pedidoService) 
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        [Route("pedido")]
        public async Task<ActionResult<Pedido>> CadastrarNovoPedido(PedidoCreate pedidoDto, int? usuarioId)
        {
            var pedido = await _pedidoService.CriarPedido(pedidoDto, usuarioId);
            return Created();
        }
    }
}
