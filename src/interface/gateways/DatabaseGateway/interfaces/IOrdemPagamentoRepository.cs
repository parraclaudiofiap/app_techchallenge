namespace DbGateway;

public interface IOrdemPagamentoRepository
{
    Task SalvarOrdemPagamento(OrdemPagamentoDAO ordemDePagamento);
    
    Task AtualizarOrdemPagamento(OrdemPagamentoDAO ordemDePagamento);

    Task<OrdemPagamentoDAO?> BuscarOrdemPagamentoPorId(string idOrdemPagamento);
}
