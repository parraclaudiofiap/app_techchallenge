using Domain.Entities;
using UserCase;

namespace DbGateway;

public class FilaPedidosGateway : IFilaPedidosGateway
{
    private readonly IFilaPedidosRepository _filaPedidosRepository;

    public FilaPedidosGateway(IFilaPedidosRepository filaPedidosRepository)
    {
        _filaPedidosRepository = filaPedidosRepository;
    }

    public async Task<IList<FilaPedidos>> PesquisarFilaDePedidos()
    {
        var db = await _filaPedidosRepository.PesquisarFilaDePedidos();

        return db.Select(f => new FilaPedidos(f.NumeroPedido, f.Prioridade, f.ExpectativaFinalizacao)).ToList();
    }

    public async Task RemoverPedidoFila(string numeroPedido)
    {
        await _filaPedidosRepository.RemoverPedidoFila(numeroPedido);
    }

    public async Task SalvarPedidoNaFila(FilaPedidos pedidos)
    {
          var filaPedidosEntity = new FilaPedidosDAO()
        {
            NumeroPedido = pedidos.NumeroPedido,
            Prioridade = pedidos.Prioridade,
            ExpectativaFinalizacao = pedidos.ExpectativaFinalizacao
        };
        
        await _filaPedidosRepository.SalvarPedidoNaFila(filaPedidosEntity);
    }
}
