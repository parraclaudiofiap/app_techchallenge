using Domain.ValueObjects;

namespace UserCase;

public class FilaPedidosDTO
{
    public string NumeroPedido { get;  set; }
    public string ProgressoPedido => Pedido.ProgressoPedido;
    public int Prioridade { get;  set; }
    public DateTime ExpectativaFinalizacao { get;  set; }

    public PedidoDTO Pedido {get; set;}


}
