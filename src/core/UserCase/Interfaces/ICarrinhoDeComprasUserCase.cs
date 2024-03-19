using Domain.Entities;

namespace UserCase.Interfaces;

public interface ICarrinhoDeComprasUserCase
{
    Task<CarrinhoDeComprasDto?> AdicionarProduto(string idAtendimento, string nomeProduto, string? cpf = null);
    
    void RemoverProduto( string idCarrinhoCompras, Produto produto);
    
    Task<CarrinhoDeComprasDto?> ExecutarCheckout(string idCarrinhoCompras);
    
    Task<CarrinhoDeComprasDto> BuscarCarrinhoDeComprasPorCpf(string cpf);

    Task<CarrinhoDeComprasDto> BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(string idCarrinhoCompras);

    Task<CarrinhoDeComprasDto?> FinalizarCarrinho(string idCarrinhoCompras);
}