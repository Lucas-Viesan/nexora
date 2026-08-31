using AutoMapper;
using Nexora.Dtos.ItemPedido;
using Nexora.Entities;

namespace Nexora.Profiles
{
    public class ItemPedidoProfile : Profile
    {
        public ItemPedidoProfile()
        {
            CreateMap<ItemPedido, ItemPedidoResponse>();
        }
    }
}
