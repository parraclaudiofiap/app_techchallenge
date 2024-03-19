namespace UserCase;

public class OrdemPagamentoDTO
{
     public string StatusPagamento{ get;  set;}
    
    public DateTime DataCriacao{ get;  set;}
    public DateTime DataAtualizacao{ get;  set;}
    
    public double ValorTotal { get;  set; }
    
    public double? ValorPago { get;  set; }
    
    public DateTime? DataPagamento{ get;  set;}

    public string NumeroPedido => IdCarrinhoDeCompras.Substring(0,4);

    public string IdCarrinhoDeCompras { get;  set; }

    public string IdOrdemPagamento { get;  set; }
}
