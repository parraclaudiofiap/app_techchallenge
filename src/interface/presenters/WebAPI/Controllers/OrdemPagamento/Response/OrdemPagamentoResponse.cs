namespace WebAPI;

public class OrdemPagamentoResponse
{

    /// <summary>
    /// Identificação da ordem de pagamento.
    /// </summary>
    public string IdOrdemPagamento { get;  set; }
    
    /// <summary>
    /// Status do pagamento retornado  pelo servico de meio de pagamento, podendo ser 
    /// Pendente = aguardando pagamento
    /// Aprovado = pagamento aprovado 
    /// Rejeitado  = pagamento declinado.
    /// </summary>
    /// 
    public string StatusPagamento{ get;  set;}

    /// <summary>
    /// Identificação do pedido (gerado quando o pagamento é aprovado)
    /// </summary>
    public string NumeroPedido {get; set;}

    /// <summary>
    /// Texto gerado randomicamente para identificar o carrinho de compras. 
    /// </summary>
    public string IdCarrinhoDeCompras { get;  set; }

    /// <summary>
    /// Data carimbada pelo sistema quando o cliente solicitou checkout
    /// </summary>
    public DateTime DataCriacao{ get;  set;}

    /// <summary>
    /// Data carimbada pelo sistema quando o servico de pagamento a resposta do processamento do pagamento
    /// </summary>    
    public DateTime DataAtualizacao{ get;  set;}
    /// <summary>
    /// Valor total da compra
    /// </summary>  
    public double ValorTotal { get;  set; }
    /// <summary>
    /// Valor total pago
    /// </summary>  
    public double? ValorPago { get;  set; }
    
    /// <summary>
    /// Data do processamento do pagagmento
    /// </summary>  
    public DateTime? DataPagamento{ get;  set;}


}
