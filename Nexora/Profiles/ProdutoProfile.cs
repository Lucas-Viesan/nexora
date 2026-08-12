using AutoMapper;
using Nexora.DTOs.Produto;
using Nexora.Entities;

namespace Nexora.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ProdutoCreate, Produto>();
            CreateMap<Produto, ProdutoResponse>();

        }

    }
}
