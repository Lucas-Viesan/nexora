using AutoMapper;
using Nexora.Dtos.Produto;
using Nexora.DTOs.Produto;
using Nexora.Entities;
using Nexora.Enums;
using Nexora.Interfaces;

namespace Nexora.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private IMapper _mapper;

    public ProdutoService(
        IProdutoRepository produtoRepository,
        IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<ProdutoResponse> CadastrarProduto(int usuarioId, ProdutoCreate produtoDto)
    {
        // Busca o usuário responsável pela operação
        var usuario =  await _usuarioRepository.BuscarPorId(usuarioId);

        if (usuario == null)
        {
            throw new KeyNotFoundException(
                "Usuário não encontrado.");
        }

        // Regra de autorização
        if (usuario.Perfil != PerfilUsuario.Administrador)
        {
            throw new UnauthorizedAccessException(
                "Somente usuários administradores podem cadastrar produtos.");
        }

        Produto produto = new Produto(
           produtoDto.Nome,
           produtoDto.Descricao,
           produtoDto.Preco
        );
        await _produtoRepository.CadastrarProduto(produto);
        var resposta = _mapper.Map<ProdutoResponse>(produto);
        return resposta;
    
    }

    public async Task<List<ProdutoResponse>> BuscarTodosProdutosDisponiveis()
    {
        var produtosDisponiveis = await _produtoRepository.VerificarProdutosDisponiveis();
        return produtosDisponiveis;
    }

    public async Task<ProdutoResponse?> AlterarInfoDosProdutos(int produtoId, int usuarioId, ProdutoAlteracaoDados produtoAlteracaoDados)
    {
        var usuario = await _usuarioRepository.BuscarPorId(usuarioId);

        if (usuario == null)
        {
            throw new KeyNotFoundException(
                "Usuário não encontrado.");
        }

        // Regra de autorização
        if (usuario.Perfil != PerfilUsuario.Administrador)
        {
            throw new UnauthorizedAccessException(
                "Somente usuários administradores podem cadastrar produtos.");
        }
        var produtoAlterado = await _produtoRepository.AlterarInfoProduto(produtoId, produtoAlteracaoDados);
        if (produtoAlterado != null) 
        {
            var resposta = _mapper.Map<ProdutoResponse>(produtoAlterado);
            return resposta;
        }
        return null;
    }


}
