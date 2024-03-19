using Domain.Entities;

namespace UserCase;

public interface IPedidoGateway
{    
    Task SalvarPedido(Pedido pedido);

    Task AtualizarPedido(Pedido pedido);

    Task<IList<Pedido>> BuscarTodos();

    Task<Pedido> BuscarPorNumeroPedido(string idPedido);
}
