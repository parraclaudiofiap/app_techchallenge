namespace WebAPI;

public class FilaPedidosData
{
    /// <summary>
    /// Identificação do pedido 
    /// </summary>
    public string NumeroPedido { get; set; }

    /// <summary>
    /// Progresso do pedido
    /// </summary>
    public string ProgressoPedido  { get; set; }
    
    /// <summary>
    ///Prioridade de atendimento do pedido
    /// </summary>
    public int Prioridade { get; set; }
    
    /// <summary>
    /// Data esperado para finalização do pedido 
    /// </summary>
    public DateTime ExpectativaFinalizacao { get; set; }

    
    /// <summary>
    /// Tempo de aguardo para finalização do pedido 
    /// </summary>
    public TimeSpan TempoEspera => CalculaTempoEspera();

    private TimeSpan CalculaTempoEspera()
    {
        return (DateTime.Now - ExpectativaFinalizacao).Duration();
    }
    
}