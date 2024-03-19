namespace UserCase;

public class PedidoDTO
{
    public string IdPedido { get;  set; }
    public string IdCarrinhoDeCompras { get;  set;}
    public string ProgressoPedido { get;  set; }
    public List<ProdutoDto> Produtos { get;  set; }

}
