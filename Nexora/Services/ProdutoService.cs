using AutoMapper;
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

    public async Task<ProdutoResponse> CadastrarProduto(
        ProdutoCreate produtoDto,
        int usuarioId)
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


}
