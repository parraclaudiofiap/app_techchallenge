using Domain.Entities;
using Domain.ValueObjects;

namespace UserCase.Interfaces;

public interface IProdutoUserCase
{
    Task CadastrarProduto(ProdutoDto produto);
    
    Task EditarProduto(ProdutoDto produto);

    Task RemoverProduto(string nome);

    Task<IList<ProdutoDto>> PesquisarProdutosPorCategoria(CategoriaProdutoEnum categoriaProdutoEnum);
    Task<ProdutoDto> PesquisarProdutoPorNome(string nomeProduto);
}   