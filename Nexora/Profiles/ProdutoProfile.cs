using AutoMapper;
using Nexora.DTOs.Produto;
using Nexora.Entities;

namespace Nexora.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ProdutoResponse, Produto>();
            CreateMap<Produto, ProdutoCreate>();

        }

    }
}
