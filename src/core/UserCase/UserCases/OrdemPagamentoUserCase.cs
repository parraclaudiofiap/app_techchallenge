using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using UserCase.Interfaces;

namespace UserCase;

public class OrdemPagamentoUserCase : IOrdemPagamentoUserCase
{
    private readonly IMeioPagamentoGateway _meioPagamentoGateway;
    private readonly IOrdemPagamentoGateway _ordemPagamentoGateway;

    private readonly ICarrinhoDeComprasGateway _carrinhoDeComprasGateway;

    private readonly IMapper _mapper;

    private readonly IPedidoUserCase _pedidoUserCase;

    public OrdemPagamentoUserCase(IMeioPagamentoGateway meioPagamentoGateway, IOrdemPagamentoGateway ordemPagamentoGateway, ICarrinhoDeComprasGateway carrinhoDeComprasGateway, IMapper mapper, IPedidoUserCase pedidoUserCase)
    {
        _meioPagamentoGateway = meioPagamentoGateway;
        _ordemPagamentoGateway = ordemPagamentoGateway;
        _carrinhoDeComprasGateway = carrinhoDeComprasGateway;
        _mapper = mapper;
        _pedidoUserCase = pedidoUserCase;
    }

    public async Task GerarOrdemPagamento(string idCarrinhoCompras, double valorTotal)
    {
        var idMeioPagamento = _meioPagamentoGateway.GerarPagamentoQRCode();
            
        var ordemDePagamento = new OrdemDePagamento(idCarrinhoCompras, idCarrinhoCompras, valorTotal);

        await _ordemPagamentoGateway.SalvarOrdemPagamento(ordemDePagamento);
    }

    public async Task AtualizarStatusPagamento(string idMeioPagamento, string statusPagamento)
    {
        var ordemDePagamento = await _ordemPagamentoGateway.BuscarOrdemPagamentoPorId(idMeioPagamento)
            ?? throw new InvalidOperationException("Ordem Pagamento não encontrado");

        var statusPagamentoEnum = Enum.Parse<StatusPagamento>(statusPagamento);
        ordemDePagamento.SetStatusPagamento(statusPagamentoEnum);

        await _ordemPagamentoGateway.AtualizarOrdemPagamento(ordemDePagamento);
        await AtualizarCarrinho(ordemDePagamento.IdCarrinhoDeCompras,  MapStatusCarrinho(statusPagamentoEnum));

    }

    
    public async Task<OrdemPagamentoDTO> BuscarPagamento(string idMeioPagamento)
    {
        var ordemDePagamento = await _ordemPagamentoGateway.BuscarOrdemPagamentoPorId(idMeioPagamento)
            ?? throw new InvalidOperationException("Ordem Pagamento não encontrado");

        return _mapper.Map<OrdemPagamentoDTO>(ordemDePagamento);
    }

    private StatusCarrinhoDeCompras MapStatusCarrinho(StatusPagamento statusPagamento)
    {
        return statusPagamento switch
        {
            StatusPagamento.Aprovado => StatusCarrinhoDeCompras.Finalizado,
            StatusPagamento.Rejeitado => StatusCarrinhoDeCompras.EmAberto,
            _ => StatusCarrinhoDeCompras.EmAberto,
        };
    }

    private async Task AtualizarCarrinho(string idCarrinhoCompras, StatusCarrinhoDeCompras statusCarrinhoDeCompras)
    {
          var dbCarrinho = await _carrinhoDeComprasGateway.BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(idCarrinhoCompras)
            ?? throw new InvalidOperationException("Carrinho não encontrado");

        dbCarrinho.SetStatus(statusCarrinhoDeCompras);  
        await _carrinhoDeComprasGateway.Atualizar(dbCarrinho);

        if(statusCarrinhoDeCompras == StatusCarrinhoDeCompras.Finalizado)
        {
            await _pedidoUserCase.GerarPedido(dbCarrinho);
        }
    }
}
