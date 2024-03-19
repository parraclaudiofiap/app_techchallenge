using Domain.Entities;
using UserCase.Interfaces;
using Domain.ValueObjects;
using AutoMapper;

namespace UserCase.UserCases;

public class ProdutoUserCase : IProdutoUserCase
{
    private readonly IProdutoGateway _produtoDbGateway;

    private readonly IMapper _mapper;

    public ProdutoUserCase(IMapper mapper, IProdutoGateway produtoDbGateway)
    {
        _produtoDbGateway = produtoDbGateway;
        _mapper = mapper;
    }

    public async Task CadastrarProduto(ProdutoDto produtoDto)
    {
       await _produtoDbGateway.CadastrarProduto(_mapper.Map<Produto>(produtoDto));
    }

    public async Task EditarProduto(ProdutoDto produtoDto)
    {
       await _produtoDbGateway.EditarProduto(_mapper.Map<Produto>(produtoDto));
    }

    public async Task RemoverProduto(string nome)
    {
        await _produtoDbGateway.RemoverProduto(nome);
    }

    public async Task<IList<ProdutoDto>> PesquisarProdutosPorCategoria(CategoriaProdutoEnum categoriaProdutoEnum)
    {
        var produtos =  await _produtoDbGateway.PesquisarProdutosPorCategoria(categoriaProdutoEnum);
       return  _mapper.Map<List<ProdutoDto>>(produtos);
    }

    public async Task<ProdutoDto> PesquisarProdutoPorNome(string nomeProduto)
    {
          var produto = await _produtoDbGateway.PesquisarProdutoPorNome(nomeProduto);
         return _mapper.Map<ProdutoDto>(produto);
    }
}