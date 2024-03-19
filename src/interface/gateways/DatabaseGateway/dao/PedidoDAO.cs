namespace DbGateway;

public class PedidoDAO : BaseDAO
{
    public string IdPedido { get;  set; }
    public string NumeroPedido { get;  set; }
    public string IdCarrinhoDeCompras { get;  set;}
    public string ProgressoPedido { get;  set; }
    public List<ProdutoDAO> Produtos { get;  set; }
}
