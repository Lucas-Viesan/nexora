using Nexora.Entities;
using Nexora.Enums;
using Nexora.Interfaces;
using Nexora.Dtos.Pedido;
using AutoMapper;

namespace Nexora.Services
{
    public class PedidoService : IPedidoService
    {   
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IClienteRepository _clienteRepository;
        private IMapper _mapper;


        public PedidoService(
            IPedidoRepository pedidoRepository,
            IProdutoRepository produtoRepository,
            IClienteRepository clienteRepository,
            IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
        public async Task<Pedido> CriarPedido(PedidoCreate pedidoDto, int? usuarioId)
        {
            if (pedidoDto.Itens is null || pedidoDto.Itens.Count == 0)
                throw new ArgumentException("O pedido deve conter ao menos um item.");

            var origem = usuarioId.HasValue ? OrigemPedido.Balcao : OrigemPedido.Online;

            Cliente? cliente = null;
            if (pedidoDto.ClienteId.HasValue)
            {
                cliente = await _clienteRepository.BuscarClientePorId(pedidoDto.ClienteId.Value);
                if (cliente is null)
                    throw new InvalidOperationException("Cliente informado não foi encontrado.");
            }

            var pedido = new Pedido(origem, cliente, usuarioId);

            foreach (var itempedidoDto in pedidoDto.Itens)
            {
                var produto = await _produtoRepository.BuscarProdutoPorId(itempedidoDto.ProdutoId);
                if (produto is null)
                    throw new InvalidOperationException($"Produto {itempedidoDto.ProdutoId} não foi encontrado.");

                pedido.AdicionarItem(produto, itempedidoDto.Quantidade, itempedidoDto.Observacao);
            }

            await _pedidoRepository.CadastrarPedido(pedido);

            return pedido;
        }

        public async Task<PedidoResponse> ConsultarPedidoId(int id)
        {
            var pedido = await _pedidoRepository.BuscarPedidoId(id);
            if (pedido is null)
            {
                return null;
            }
          var pedidoResponse  = _mapper.Map<PedidoResponse>(pedido);
        return pedidoResponse;
        }
    }
}
