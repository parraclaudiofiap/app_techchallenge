using Domain.Entities;
using Domain.ValueObjects;

namespace WebApi.Controllers.CarrinhoDeCompras.Response;

public class CarrinhoDeComprasResponse
{
    /// <summary>
    /// Texto gerado randomicamente para identificar o carrinho de compras. 
    /// </summary>
    public string IdCarrinhoDeCompras { get;  set; }
    
    /// <summary>
    /// Campo booleano indicando se o cliente optou por se identificar ou não
    /// </summary>
    public bool ClienteIdentificado => (CPF is not null);
    
    /// <summary>
    /// Numero de identificação do cliente 
    /// </summary>
    public string? CPF { get;  set;}

    /// <summary>
    /// Status do carrinho de compras, podendo ser EmAberto = Cliente esta no processo de compra ou Finalizado = cliente finalizou o pedido.
    /// </summary>
    public StatusCarrinhoDeCompras Status { get;  set; }
    
    /// <summary>
    /// Identificação da ordem de pagamento.
    /// </summary>
    public OrdemDePagamento OrdemDePagamento { get;  set;}
    
    
    /// <summary>
    /// Lista de produtos adicionado no carrinho de compras
    /// </summary>
    public List<Domain.Entities.Produto> Produtos { get;  set; }

    /// <summary>
    /// Identificação do pedido (gerado quando realizado o checkout-pagamento)
    /// </summary>
    public string NumeroPedido { get;  set; }
    
    /// <summary>
    /// Valor total de venda
    /// </summary>
    public double SomaDoPreco { get;  set; }
    
    /// <summary>
    /// Quantidade total de produtos no carrinho
    /// </summary>
    public int QuantidadeItens { get;  set; }
}