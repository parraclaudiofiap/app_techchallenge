using Domain.Entities;
using Domain.ValueObjects;

namespace UserCase;

public interface IProdutoGateway
{
    Task CadastrarProduto(Produto produto);
    
    Task EditarProduto(Produto produto);

    Task RemoverProduto(string nome);

    Task<IList<Produto>> PesquisarProdutosPorCategoria(CategoriaProdutoEnum categoriaProdutoEnum);
    Task<Produto> PesquisarProdutoPorNome(string nomeProduto);
}
