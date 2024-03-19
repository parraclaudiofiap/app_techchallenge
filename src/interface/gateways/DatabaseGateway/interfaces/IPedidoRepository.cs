namespace DbGateway;

public interface IPedidoRepository
{
    Task SalvarPedido(PedidoDAO pedido);

    Task<IList<PedidoDAO>> BuscarTodos();

    Task<PedidoDAO> BuscarPorNumeroPedido(string idPedido);

      Task AtualizarPedido(PedidoDAO pedido);
}
