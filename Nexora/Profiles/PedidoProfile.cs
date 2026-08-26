using AutoMapper;
using Nexora.Dtos.Pedido;
using Nexora.Entities;

namespace Nexora.Profiles
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<Pedido, PedidoResponse>();
        }
    }
}
