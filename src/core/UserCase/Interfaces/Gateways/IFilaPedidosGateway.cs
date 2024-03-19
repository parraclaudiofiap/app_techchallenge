using Domain.Entities;

namespace UserCase;

public interface IFilaPedidosGateway
{
   Task SalvarPedidoNaFila(FilaPedidos pedidos);

   Task RemoverPedidoFila(string idPedido);
   
   Task<IList<FilaPedidos>> PesquisarFilaDePedidos();
}
