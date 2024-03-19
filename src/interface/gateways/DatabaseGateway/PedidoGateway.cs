using Domain.Entities;
using Domain.ValueObjects;
using UserCase;

namespace DbGateway;

public class PedidoGateway : IPedidoGateway
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoGateway(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task AtualizarPedido(Pedido pedido)
    {
        var produtos = pedido.Produtos.Select(p => new ProdutoDAO()
        {
            Nome = p.Nome,
            Categoria = p.Categoria.ToString(),
            Descricao = p.Descricao,
            Imagem = p.Imagem,
            Preco = p.Preco
        }).ToList();


        var pedidoDAO = new PedidoDAO()
        {
            Id = pedido.IdCarrinhoDeCompras,
            IdCarrinhoDeCompras = pedido.IdCarrinhoDeCompras,
            IdPedido = pedido.IdPedido,
            NumeroPedido = pedido.NumeroPedido,
            ProgressoPedido = pedido.ProgressoPedido.ToString(),
            Produtos = produtos
        };

        await _pedidoRepository.AtualizarPedido(pedidoDAO);
    }

    public async Task<Pedido> BuscarPorNumeroPedido(string idPedido)
    {
      var dbPedido = await _pedidoRepository.BuscarPorNumeroPedido(idPedido);

        return 
                new Pedido(dbPedido.IdPedido,
                    dbPedido.IdCarrinhoDeCompras,
                    Enum.Parse<ProgressoPedido>(dbPedido.ProgressoPedido))
                {
                    Produtos = dbPedido.Produtos.Select(p => new Produto(p.Nome, p.Descricao, Enum.Parse<CategoriaProdutoEnum>(p.Categoria), p.Preco, p.Imagem))
                        .ToList()
                };
    }

    public async Task<IList<Pedido>> BuscarTodos()
    {
      var dbPedidos =  await _pedidoRepository.BuscarTodos();

        return dbPedidos.Select(x =>
                new Pedido(x.IdPedido,
                    x.IdCarrinhoDeCompras,
                    Enum.Parse<ProgressoPedido>(x.ProgressoPedido))
                {
                    Produtos = x.Produtos.Select(p => new Produto(p.Nome, p.Descricao, Enum.Parse<CategoriaProdutoEnum>(p.Categoria), p.Preco, p.Imagem))
                        .ToList()
                }).ToList(); 
    }

    public async Task SalvarPedido(Pedido pedido)
    {
        var produtos = pedido.Produtos.Select(p => new ProdutoDAO()
                {
                    Nome = p.Nome,
                    Categoria = p.Categoria.ToString(),
                    Descricao = p.Descricao,
                    Imagem = p.Imagem,
                    Preco = p.Preco
                }).ToList();


        var pedidoDAO = new PedidoDAO()
        {
            Id = pedido.IdCarrinhoDeCompras,
            IdCarrinhoDeCompras = pedido.IdCarrinhoDeCompras,
            IdPedido = pedido.IdPedido,
            NumeroPedido = pedido.NumeroPedido,
            ProgressoPedido = pedido.ProgressoPedido.ToString(),
            Produtos = produtos
        };

        await _pedidoRepository.SalvarPedido(pedidoDAO);
    }


}
