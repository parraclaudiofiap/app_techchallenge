namespace WebApi.Controllers.CarrinhoDeCompras.Response;

public class CheckoutResponse
{
    /// <summary>
    /// Identificação do pedido (gerado quando realizado o checkout-pagamento)
    /// </summary>
    public string NumeroPedido { get;  set; }
    /// <summary>
    /// Valor total de venda
    /// </summary>
    
    public double SomaDoPreco { get;  set; }
    
    
    /// <summary>
    /// Quantidade total de produtos no pedido
    /// </summary>
    public int QuantidadeItens { get;  set; }
    
      
    /// <summary>
    /// Lista de produtos requisitado
    /// </summary>
    
    public List<Domain.Entities.Produto> Produtos { get;  set; }
}