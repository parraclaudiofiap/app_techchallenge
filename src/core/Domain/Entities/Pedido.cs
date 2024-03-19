using Domain.ValueObjects;

namespace Domain.Entities;

public class Pedido
{
    public Pedido(string idCarrinhoDeCompras, ProgressoPedido progressoPedido, List<Produto> produtos)
    {
        IdPedido = idCarrinhoDeCompras;
        IdCarrinhoDeCompras = idCarrinhoDeCompras;
        ProgressoPedido = progressoPedido;
        Produtos = produtos;
    }
    
    public Pedido(string idPedido, string idCarrinhoDeCompras, ProgressoPedido progressoPedido)
    {
        IdPedido = idPedido;
        IdCarrinhoDeCompras = idCarrinhoDeCompras;
        ProgressoPedido = progressoPedido;
    }


    public string IdPedido { get; private set; }
    public string NumeroPedido => IdPedido.Substring(0,4);
    public string IdCarrinhoDeCompras { get; private set;}
    public ProgressoPedido ProgressoPedido { get; private set; }
    public List<Produto> Produtos { get;  set; }

    public void SetProgressoPedido(string progressoPedido)
    {
        ProgressoPedido = Enum.Parse<ProgressoPedido>(progressoPedido);
    }
}