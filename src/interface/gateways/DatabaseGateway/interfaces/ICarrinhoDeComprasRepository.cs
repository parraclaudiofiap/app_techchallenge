namespace DbGateway;

public interface ICarrinhoDeComprasRepository
{
    Task Atualizar(CarrinhoDeComprasDAO carrinhoDeCompras);

    Task<CarrinhoDeComprasDAO?> BuscarCarrinhoDeComprasPorCpf(string cpf);

    Task<CarrinhoDeComprasDAO?> BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(string? idCarrinhoDeCompras);
}
