using AutoMapper;
using Domain.Entities;
using UserCase.Interfaces;


namespace UserCase.UserCases;

public class CarrinhoDeComprasUserCase: ICarrinhoDeComprasUserCase
{
    private readonly ICarrinhoDeComprasGateway _dbGateway;
    private readonly IOrdemPagamentoUserCase _ordemPagamentoUserCase;
    private readonly IClienteUserCase _clienteUserCase;
    private readonly IProdutoUserCase _produtoUserCase;
    private readonly IMapper _mapper;

    public CarrinhoDeComprasUserCase(ICarrinhoDeComprasGateway dbGateway, IClienteUserCase clienteUserCase, IProdutoUserCase produtoUserCase, IMapper mapper, IOrdemPagamentoUserCase ordemPagamentoUserCase)
    {
        _dbGateway = dbGateway;
        _clienteUserCase = clienteUserCase;
        _produtoUserCase = produtoUserCase;
        _mapper = mapper;
        _ordemPagamentoUserCase = ordemPagamentoUserCase;
    }

    public async Task<CarrinhoDeComprasDto?> AdicionarProduto(string idAtendimento, string NomeProduto, string? cpf = null)
    {
        var dbCarrinho = new CarrinhoDeCompras(idAtendimento);
        
        if (!string.IsNullOrEmpty(cpf))
        {
            // TODO: adicionar pesquisa de carrinho por idAtendimento 
            dbCarrinho = await _dbGateway.BuscarCarrinhoDeComprasPorCpf(cpf);
            
            if (dbCarrinho is null)
            {
                var dbCliente = _clienteUserCase.PesquisarPorCpf(cpf);
                if (dbCliente is null)
                {
                    throw new InvalidOperationException("Cliente não encontrado");
                };
                dbCarrinho = new CarrinhoDeCompras(idAtendimento, cpf);
            }
        }

// TODO: pesquisar carrinho por idAtendimento 
        var dbProduto =  await _produtoUserCase.PesquisarProdutoPorNome(NomeProduto);
        if (dbProduto is null)
        {
            throw new InvalidOperationException("Produto não encontrado");
        };
        
        dbCarrinho.Produtos.Add(_mapper.Map<Produto>(dbProduto));
        
        await _dbGateway.Atualizar(dbCarrinho);

       return  _mapper.Map<CarrinhoDeComprasDto>(dbCarrinho);
    }

    public async Task<CarrinhoDeComprasDto> BuscarCarrinhoDeComprasPorCpf(string cpf)
    {
       var dbCarrinho = await _dbGateway.BuscarCarrinhoDeComprasPorCpf(cpf) 
            ?? throw new InvalidOperationException("Carrinho não encontrado");
        
          return  _mapper.Map<CarrinhoDeComprasDto>(dbCarrinho);
    }

    public async  Task<CarrinhoDeComprasDto> BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(string idCarrinhoCompras)
    {
        var dbCarrinho = await _dbGateway.BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(idCarrinhoCompras) 
            ?? throw new InvalidOperationException("Carrinho não encontrado");
        
          return  _mapper.Map<CarrinhoDeComprasDto>(dbCarrinho);
    }

    public void RemoverProduto(string idCarrinhoCompras, Produto produto)
    {
        throw new NotImplementedException();
    }
    
    public async Task<CarrinhoDeComprasDto?> ExecutarCheckout(string idCarrinhoCompras)
    {
        var dbCarrinho = await _dbGateway.BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(idCarrinhoCompras)
            ?? throw new InvalidOperationException("Carrinho não encontrado");

        await _ordemPagamentoUserCase.GerarOrdemPagamento(idCarrinhoCompras, dbCarrinho.SomaDoPreco);

        await _dbGateway.Atualizar(dbCarrinho);
        
        return  _mapper.Map<CarrinhoDeComprasDto>(dbCarrinho);
    }

        
    public async Task<CarrinhoDeComprasDto?> FinalizarCarrinho(string idCarrinhoCompras)
    {
        var dbCarrinho = await _dbGateway.BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(idCarrinhoCompras)
            ?? throw new InvalidOperationException("Carrinho não encontrado");

        await _ordemPagamentoUserCase.GerarOrdemPagamento(idCarrinhoCompras, dbCarrinho.SomaDoPreco);

        dbCarrinho.SetStatus( Domain.ValueObjects.StatusCarrinhoDeCompras.Finalizado);  
        await _dbGateway.Atualizar(dbCarrinho);
        
        return  _mapper.Map<CarrinhoDeComprasDto>(dbCarrinho);
    }
}
