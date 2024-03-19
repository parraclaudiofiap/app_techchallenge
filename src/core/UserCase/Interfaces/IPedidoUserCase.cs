using Domain.Entities;

namespace UserCase.Interfaces;

public interface IPedidoUserCase
{
    Task<string> GerarPedido(CarrinhoDeCompras carrinhoDeCompras);

    Task<IList<FilaPedidosDTO>> BuscarTodosNaFila();

    Task<IList<PedidoDTO>> BuscarTodos();
    
    Task<PedidoDTO> AtualizarProgressoPedido(string idPedido, string progressoPedido);
}