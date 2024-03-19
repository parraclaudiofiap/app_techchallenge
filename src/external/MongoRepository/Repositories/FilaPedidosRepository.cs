using DbGateway;
using MongoRepository.Context;
using MongoRepository.Repositories;

namespace MongoRepository;

public class FilaPedidosRepository : BaseRepository<FilaPedidosDAO>, IFilaPedidosRepository
{
    public FilaPedidosRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IList<FilaPedidosDAO>> PesquisarFilaDePedidos()
    {
        return  GetList(_context.FilaPedidos, _ => true);
    }

    public async Task RemoverPedidoFila(string numeroPedido)
    {
       await Delete(_context.FilaPedidos, fp => fp.NumeroPedido == numeroPedido);
    }

    public async Task SalvarPedidoNaFila(FilaPedidosDAO pedidos)
    {
        await InsertOne(_context.FilaPedidos, pedidos);
    }
}
