using Domain.Entities;

namespace UserCase;

public interface ICarrinhoDeComprasGateway
{

     Task Atualizar(CarrinhoDeCompras carrinhoDeCompras);
    
    Task<CarrinhoDeCompras?> BuscarCarrinhoDeComprasPorCpf(string cpf);
    
    Task<CarrinhoDeCompras?> BuscarCarrinhoDeComprasPorId(string id);

    Task<CarrinhoDeCompras?> BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(string? idCarrinhoDeCompras);
}
