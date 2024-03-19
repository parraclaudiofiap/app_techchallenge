using Domain.Entities;
using Domain.ValueObjects;
using UserCase;

namespace DbGateway;

public class OrdemPagamentoGateway : IOrdemPagamentoGateway
{
    private IOrdemPagamentoRepository _ordemPagamentoRepository;

    public OrdemPagamentoGateway(IOrdemPagamentoRepository ordemPagamentoRepository)
    {
        _ordemPagamentoRepository = ordemPagamentoRepository;
    }

    public async Task AtualizarOrdemPagamento(OrdemDePagamento ordemDePagamento)
    {
        var ordemDePagamentoDAO = new OrdemPagamentoDAO()
        {
            Id = ordemDePagamento.IdOrdemPagamento,
            IdOrdemPagamento = ordemDePagamento.IdOrdemPagamento,
            DataCriacao = ordemDePagamento.DataCriacao,
            IdCarrinhoDeCompras = ordemDePagamento.IdCarrinhoDeCompras,
            StatusPagamento = ordemDePagamento.StatusPagamento.ToString(),
            ValorTotal = ordemDePagamento.ValorTotal,
            DataAtualizacao = ordemDePagamento.DataAtualizacao,
            ValorPago = ordemDePagamento.ValorPago,
            DataPagamento = ordemDePagamento.DataPagamento
        }; 

        await _ordemPagamentoRepository.AtualizarOrdemPagamento(ordemDePagamentoDAO);
    }

    public async Task<OrdemDePagamento?> BuscarOrdemPagamentoPorId(string idOrdemPagamento)
    {
        var op = await _ordemPagamentoRepository.BuscarOrdemPagamentoPorId(idOrdemPagamento);
        if(op is null)
        {
            return null;
        }

        return new OrdemDePagamento(op.IdOrdemPagamento, 
            op.IdCarrinhoDeCompras,
            Enum.Parse<StatusPagamento>(op.StatusPagamento), 
            op.DataCriacao, 
            op.ValorTotal,
            op.DataAtualizacao,
            op.DataPagamento, 
            op.ValorPago);
    }

    public async Task SalvarOrdemPagamento(OrdemDePagamento ordemDePagamento)
    {
        var ordemDePagamentoDAO = new OrdemPagamentoDAO()
       {
            Id = ordemDePagamento.IdOrdemPagamento,
            IdOrdemPagamento = ordemDePagamento.IdOrdemPagamento,
            DataCriacao = ordemDePagamento.DataCriacao,
            IdCarrinhoDeCompras = ordemDePagamento.IdCarrinhoDeCompras,
            StatusPagamento = ordemDePagamento.StatusPagamento.ToString(),
            ValorTotal = ordemDePagamento.ValorTotal
        }; 

        await _ordemPagamentoRepository.SalvarOrdemPagamento(ordemDePagamentoDAO);
    }
}
