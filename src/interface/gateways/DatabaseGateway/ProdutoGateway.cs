using Domain.Entities;
using Domain.ValueObjects;
using UserCase;

namespace DbGateway;

public class ProdutoGateway : IProdutoGateway
{
    private IProdutoRepository _produtoRepository;

    public ProdutoGateway(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task CadastrarProduto(Produto produto)
    {
        var produtoDao = new ProdutoDAO()
        {
            Nome = produto.Nome,
            Categoria = produto.Categoria.ToString(),
            Descricao = produto.Descricao,
            Imagem = produto.Imagem,
            Preco = produto.Preco
        };

        await _produtoRepository.Inserir(produtoDao);
    }

    public async Task EditarProduto(Produto produto)
    {
        var produtoDao = new ProdutoDAO()
        {
            Nome = produto.Nome,
            Categoria = produto.Categoria.ToString(),
            Descricao = produto.Descricao,
            Imagem = produto.Imagem,
            Preco = produto.Preco
        };

        await _produtoRepository.EditarProduto(produtoDao);
    }

    public async Task<Produto> PesquisarProdutoPorNome(string nomeProduto)
    {
        var dbProduto = await _produtoRepository.PesquisarProdutoPorNome(nomeProduto);

        return new Produto(dbProduto.Nome, dbProduto.Descricao, Enum.Parse<CategoriaProdutoEnum>(dbProduto.Categoria), dbProduto.Preco, dbProduto.Imagem);
    }

    public async Task<IList<Produto>> PesquisarProdutosPorCategoria(CategoriaProdutoEnum categoriaProdutoEnum)
    {
        var dbProdutos = await _produtoRepository.PesquisarProdutosPorCategoria(categoriaProdutoEnum.ToString());

        return dbProdutos.Select( dbProduto => new Produto(dbProduto.Nome, dbProduto.Descricao, Enum.Parse<CategoriaProdutoEnum>(dbProduto.Categoria), dbProduto.Preco, dbProduto.Imagem)).ToList();
    }

    public async Task RemoverProduto(string nome)
    {
       await _produtoRepository.RemoverProduto(nome);
    }
}
