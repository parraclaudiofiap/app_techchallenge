using Domain.Entities;
using UserCase.Interfaces;
using Domain.ValueObjects;
using AutoMapper;


namespace UserCase.UserCases;


public class PedidoUserCase : IPedidoUserCase
{
    private readonly IPedidoGateway _pedidoGateway;
    private readonly IFilaPedidosGateway _filaPedidosGateway;
    private readonly IMapper _mapper;

    public PedidoUserCase(IPedidoGateway pedidoGateway, IFilaPedidosGateway filaPedidosGateway, IMapper mapper)
    {
        _pedidoGateway = pedidoGateway;
        _filaPedidosGateway = filaPedidosGateway;
        _mapper = mapper;
    }

    public async Task<string> GerarPedido(CarrinhoDeCompras carrinhoDeCompras)
    {
        var pedido = new Pedido(carrinhoDeCompras.IdCarrinhoDeCompras, ProgressoPedido.Recebido,
            carrinhoDeCompras.Produtos);
        
        await _pedidoGateway.SalvarPedido(pedido);
        
        var filaPedidos = new FilaPedidos(pedido.NumeroPedido, 1);
        
        await _filaPedidosGateway.SalvarPedidoNaFila(filaPedidos);
        
        return pedido.NumeroPedido;
    }

    public async Task<PedidoDTO> AtualizarProgressoPedido(string numeroPedido, string progressoPedido)
    {
        var dbPedido = await _pedidoGateway.BuscarPorNumeroPedido(numeroPedido);

        dbPedido.SetProgressoPedido(progressoPedido);

        if(progressoPedido == "Finalizado")
        {
            await _filaPedidosGateway.RemoverPedidoFila(dbPedido.NumeroPedido);
        }
      
      await _pedidoGateway.AtualizarPedido(dbPedido);
      return _mapper.Map<PedidoDTO>(dbPedido);
    }

    public async Task<IList<FilaPedidosDTO>> BuscarTodosNaFila()
    {
        var dbFilaPedidos = await  _filaPedidosGateway.PesquisarFilaDePedidos();
        var dbPedidos = await  _pedidoGateway.BuscarTodos();

        var filaPedidosDTO = new List<FilaPedidosDTO>();

        foreach(var dbFilaPedido in dbFilaPedidos)
        {
            var filaPedidoDTO = _mapper.Map<FilaPedidosDTO>(dbFilaPedido);

            filaPedidoDTO.Pedido = _mapper.Map<PedidoDTO>(dbPedidos
            .First(p => p.NumeroPedido == filaPedidoDTO.NumeroPedido));

            filaPedidosDTO.Add(filaPedidoDTO);
        }
            
        var orderByProgressoPedido = new List<string> { 
            ProgressoPedido.Pronto.ToString(),
            ProgressoPedido.EmPreparacao.ToString(), 
            ProgressoPedido.Recebido.ToString() };

       return filaPedidosDTO.OrderBy(i => orderByProgressoPedido.IndexOf(i.ProgressoPedido))
            .ThenBy(i => i.ExpectativaFinalizacao)
            .ToList();    
    }

    public async Task<IList<PedidoDTO>> BuscarTodos()
    {
        var dbPedido = await _pedidoGateway.BuscarTodos();
        return _mapper.Map<List<PedidoDTO>>(dbPedido);
    }
}