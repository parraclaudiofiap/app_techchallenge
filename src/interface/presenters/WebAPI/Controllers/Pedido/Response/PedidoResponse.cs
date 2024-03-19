using UserCase;

namespace WebAPI;

public class PedidoResponse
{
    /// <summary>
    /// Identificação completa do pedido 
    /// </summary>
    public string IdPedido { get;  set; }
    /// <summary>
    /// Identificação do pedido - 4 primeiros digitos 
    /// </summary>
    public string NumeroPedido { get; set; }
    /// <summary>
    /// Identificação do carrinho de compras 
    /// </summary>
    public string IdCarrinhoDeCompras { get;  set;}
    /// <summary>
    /// Progresso do pedido
    /// </summary>
    public string ProgressoPedido { get;  set; }
    /// <summary>
    /// Lista de produtos no pedido
    /// </summary>
    public List<ProdutoDto> Produtos { get;  set; }
}