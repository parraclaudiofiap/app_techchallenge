namespace UserCase;

public interface IOrdemPagamentoUserCase
{
    Task GerarOrdemPagamento(string idCarrinhoCompras, double valorTotal);

    Task AtualizarStatusPagamento(string idMeioPagamento, string statusPagamento);

    Task<OrdemPagamentoDTO> BuscarPagamento(string idMeioPagamento);
}
