using Domain.Entities;
using Domain.ValueObjects;
using UserCase;

namespace DbGateway;

public class CarrinhoDeComprasGateway : ICarrinhoDeComprasGateway
{
    private readonly ICarrinhoDeComprasRepository _carrinhoDeComprasRepository;

    public CarrinhoDeComprasGateway(ICarrinhoDeComprasRepository carrinhoDeComprasRepository)
    {
        _carrinhoDeComprasRepository = carrinhoDeComprasRepository;
    }

    public async Task Atualizar(CarrinhoDeCompras carrinhoDeCompras)
    {
        var carrinhoDeComprasDAO = new CarrinhoDeComprasDAO()
        {
            // ID 
            CPF = carrinhoDeCompras.CPF,
            IdAtendimento = carrinhoDeCompras.IdAtendimento,
            Id = carrinhoDeCompras.IdCarrinhoDeCompras,
            IdCarrinhoDeCompras = carrinhoDeCompras.IdCarrinhoDeCompras,
            Status = carrinhoDeCompras.Status.ToString(),
            Produtos = carrinhoDeCompras.Produtos.Select(produto => new ProdutoDAO()
                {
                    Nome = produto.Nome,
                    Categoria = produto.Categoria.ToString(),
                    Descricao = produto.Descricao,
                    Imagem = produto.Imagem,
                    Preco = produto.Preco
                }).ToList()   
        };

        await _carrinhoDeComprasRepository.Atualizar(carrinhoDeComprasDAO);
    }

    public async Task<CarrinhoDeCompras?> BuscarCarrinhoDeComprasPorCpf(string cpf)
    {
        var carrinhoDeComprasDAO = await _carrinhoDeComprasRepository.BuscarCarrinhoDeComprasPorCpf(cpf);

        return MapCarrinho(carrinhoDeComprasDAO); 
    }

    public Task<CarrinhoDeCompras?> BuscarCarrinhoDeComprasPorId(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<CarrinhoDeCompras?> BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(string? idCarrinhoDeCompras)
    {
        var carrinhoDeComprasDAO = await _carrinhoDeComprasRepository.BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(idCarrinhoDeCompras);

        return MapCarrinho(carrinhoDeComprasDAO); 
    }

    private CarrinhoDeCompras? MapCarrinho(CarrinhoDeComprasDAO? carrinhoDeComprasDAO)
    {
        if(carrinhoDeComprasDAO is null)
            return null;

        var produtos =  carrinhoDeComprasDAO.Produtos.Select( dbProduto => new Produto(dbProduto.Nome, dbProduto.Descricao, Enum.Parse<CategoriaProdutoEnum>(dbProduto.Categoria), dbProduto.Preco, dbProduto.Imagem)).ToList(); 
      
        return new CarrinhoDeCompras(carrinhoDeComprasDAO.IdAtendimento,
                carrinhoDeComprasDAO.IdCarrinhoDeCompras,
                produtos,
                carrinhoDeComprasDAO.CPF,
                Enum.Parse<StatusCarrinhoDeCompras>(carrinhoDeComprasDAO.Status)
                );        
    }
}
