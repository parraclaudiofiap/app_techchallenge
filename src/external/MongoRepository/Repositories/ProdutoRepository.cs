using DbGateway;
using MongoRepository.Context;
using MongoRepository.Repositories;

namespace MongoRepository;

public class ProdutoRepository : BaseRepository<ProdutoDAO>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task EditarProduto(ProdutoDAO produto)
    {
       await Update(_context.Produtos, produto, p=> p.Nome == produto.Nome);
    }

    public async Task<bool> Inserir(ProdutoDAO produto)
    {
        return await InsertOne(_context.Produtos, produto);
    }

    public async Task<ProdutoDAO> PesquisarProdutoPorNome(string nomeProduto)
    {
        return await GetOne(_context.Produtos, p => p.Nome == nomeProduto);
    }

    public async Task<IList<ProdutoDAO>> PesquisarProdutosPorCategoria(string categoriaProdutoEnum)
    {
        return  GetList(_context.Produtos, p => p.Categoria == categoriaProdutoEnum);
    }

    public async Task RemoverProduto(string nome)
    {
        await Delete(_context.Produtos, p => p.Nome == nome);
    }
}
