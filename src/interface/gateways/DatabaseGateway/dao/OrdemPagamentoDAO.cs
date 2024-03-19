namespace DbGateway;

public class OrdemPagamentoDAO : BaseDAO
{

    public string StatusPagamento{ get; set;}
    
    public DateTime DataCriacao{ get;  set;}
    
    public DateTime DataAtualizacao { get;  set;}
    
    public double ValorTotal { get;  set; }
    
    public double? ValorPago { get;  set; }
    
    public DateTime? DataPagamento{ get;  set;}

    public string IdCarrinhoDeCompras { get;  set; }

    public string IdOrdemPagamento { get;  set; }
}
