using DbGateway;
using MongoRepository.Context;
using MongoRepository.Repositories;

namespace MongoRepository;

public class PedidoRepository : BaseRepository<PedidoDAO>, IPedidoRepository
{
    public PedidoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AtualizarPedido(PedidoDAO pedido)
    {
        await Update(_context.Pedido, pedido, p => p.IdPedido == pedido.IdPedido);
    }

    public async Task<PedidoDAO> BuscarPorNumeroPedido(string numeroPedido)
    {
       return GetList(_context.Pedido, p => p.NumeroPedido == numeroPedido).FirstOrDefault();
    }

    public async Task<IList<PedidoDAO>> BuscarTodos()
    {
       return GetList(_context.Pedido, _ => true);
    }

    public async Task SalvarPedido(PedidoDAO pedido)
    {
        await InsertOne(_context.Pedido, pedido);
    }
}
