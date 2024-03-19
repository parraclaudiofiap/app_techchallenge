namespace DbGateway;

public class FilaPedidosDAO : BaseDAO
{
   public string NumeroPedido { get;  set; }
    
    public int Prioridade { get;  set; }
    public DateTime ExpectativaFinalizacao { get;  set; }
}
