using Domain.Entities;

namespace UserCase;

public interface IOrdemPagamentoGateway
{
    Task SalvarOrdemPagamento(OrdemDePagamento ordemDePagamento);
    
    Task AtualizarOrdemPagamento(OrdemDePagamento ordemDePagamento);

    Task<OrdemDePagamento?> BuscarOrdemPagamentoPorId(string idOrdemPagamento);
}
