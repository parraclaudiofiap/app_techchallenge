namespace Domain.Entities;

public class FilaPedidos
{
    public string NumeroPedido { get; private set; }
    
    public int Prioridade { get; private set; }
    public DateTime ExpectativaFinalizacao { get; private set; }
    
    public TimeSpan TempoEspera => CalculaTempoEspera();

    public FilaPedidos(string numeroPedido, int prioridade)
    {
        NumeroPedido = numeroPedido;
        Prioridade = prioridade;
        ExpectativaFinalizacao = DateTime.Now.AddMinutes(10);
    }

    public FilaPedidos(string numeroPedido, int prioridade, DateTime expectativaFinalizacao)
    {
        NumeroPedido = numeroPedido;
        Prioridade = prioridade;
        ExpectativaFinalizacao = expectativaFinalizacao;
    }
    
    private TimeSpan CalculaTempoEspera()
    {
        return (DateTime.Now - ExpectativaFinalizacao).Duration();
    }
}