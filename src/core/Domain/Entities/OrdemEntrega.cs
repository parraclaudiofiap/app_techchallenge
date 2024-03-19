using Domain.ValueObjects;

namespace Domain.Entities;

public class OrdemEntrega
{
    public OrdemEntrega(Guid numeroPedido )
    {
        NumeroPedido = numeroPedido;
        StatusEntrega = StatusEntrega.AguardandoRetirada;
    }

    public Guid NumeroPedido { get; private set;}
    public StatusEntrega StatusEntrega { get; private set; }

    public void SetEntregaRealizada()
    {
        StatusEntrega = StatusEntrega.EntregaRealizada;
    }
}