using Microsoft.AspNetCore.Mvc;
using Nexora.Dtos.ItemPedido;
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
        public async Task<ActionResult<Pedido>> CadastrarNovoPedido([FromBody] PedidoCreate pedidoDto, [FromQuery] int? usuarioId)
        {
            var pedido = await _pedidoService.CriarPedido(pedidoDto, usuarioId);
            return Created();
        }

        [HttpGet]
        [Route("pedido/{id}")]
        public async Task<ActionResult<PedidoResponse>> ConsultarPedidoId(int id)
        {
            var pedido = await _pedidoService.ConsultarPedidoId(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
        [HttpPost]
        [Route("pedido/{id}/itens")]
        public async Task<ActionResult<PedidoResponse>> AdicionarItemAoPedido(int id, [FromBody] ItemPedidoCreate itemPedidoDto)
        {
            try
            {
                var pedido = await _pedidoService.AdicionarItemAoPedido(id, itemPedidoDto);
                return Ok(pedido);
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("pedido/{id}/itens/{itemId}")]
        public async Task<ActionResult<PedidoResponse>> AlterarQuantidadeItemPedido(int id, int itemId, [FromBody] ItemPedidoAlterarQuantidade itemPedidoQuantidadeDto) 
        {
            try
            {
                var pedido = await _pedidoService.AlterarQuantidadeItemPedido(id, itemId, itemPedidoQuantidadeDto);
                return Ok(pedido);
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut]
        [Route("pedido/{id}/cancelar")]
        public async Task<ActionResult<PedidoResponse>> CancelarPedido(int id)
        {
            try
            {
                var pedido = await _pedidoService.CancelarPedido(id);
                return Ok(pedido);
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpDelete]
        [Route("pedido/{id}/itens/{itemId}")]
        public async Task<ActionResult<PedidoResponse>> RemoverItemPedido(int id, int itemId)
        {
            try
            {
                var pedido = await _pedidoService.RemoverItemPedido(id, itemId);
                return Ok(pedido);
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
