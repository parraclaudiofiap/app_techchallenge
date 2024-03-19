using Domain.ValueObjects;

namespace Domain.Entities;

public class OrdemDePagamento
{
    public StatusPagamento StatusPagamento{ get; private set;}
    
    public DateTime DataCriacao{ get; private set;}
    public DateTime DataAtualizacao{ get; private set;}
    
    public double ValorTotal { get; private set; }
    
    public double? ValorPago { get; private set; }
    
    public DateTime? DataPagamento{ get; private set;}

    public string NumeroPedido => IdCarrinhoDeCompras.Substring(0,4);

    public string IdCarrinhoDeCompras { get; private set; }

    public string IdOrdemPagamento { get; private set; }

    public OrdemDePagamento(string idOrdemPagamento, string idCarrinhoDeCompras, double valorTotal)
    {
        IdOrdemPagamento = idOrdemPagamento;
        IdCarrinhoDeCompras = idCarrinhoDeCompras;
        StatusPagamento = StatusPagamento.Pendente;
        DataCriacao = DateTime.Now;
        DataAtualizacao = DateTime.Now;
        ValorTotal = valorTotal;
    }
    
    public OrdemDePagamento(string idOrdemPagamento, string idCarrinhoDeCompras, StatusPagamento statusPagamento, DateTime dataCriacao, double valorTotal ,  DateTime dataAtualizacao, DateTime? dataPagamento,  double? valorPago )
    {
        IdOrdemPagamento = idOrdemPagamento;
        StatusPagamento = statusPagamento;
        DataCriacao = dataCriacao;
        DataAtualizacao = dataAtualizacao;
        IdCarrinhoDeCompras = idCarrinhoDeCompras;
        ValorTotal = valorTotal;
    
        DataPagamento = dataPagamento;
        ValorPago = valorPago ;
    }

    public void SetStatusPagamento(StatusPagamento statusPagamento)
    {
        StatusPagamento = statusPagamento;
        DataAtualizacao = DateTime.Now;
        
        if(statusPagamento == StatusPagamento.Aprovado)
        {
            DataPagamento = DateTime.Now;
            ValorPago = ValorTotal;
        }
    }
}