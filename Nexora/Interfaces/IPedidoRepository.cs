using Nexora.Entities;

namespace Nexora.Interfaces
{
    public interface IPedidoRepository
    {
       public Task CadastrarPedido(Pedido pedido);
       public Task SalvarAlteracoes();
    }
}
