namespace DbGateway;

public interface IFilaPedidosRepository
{
    Task SalvarPedidoNaFila(FilaPedidosDAO pedidos);
   
   Task<IList<FilaPedidosDAO>> PesquisarFilaDePedidos();

   Task RemoverPedidoFila(string numeroPedido);
}
