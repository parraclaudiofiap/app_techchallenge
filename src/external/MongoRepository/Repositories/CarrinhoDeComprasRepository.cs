using DbGateway;
using MongoRepository.Context;
using MongoRepository.Repositories;

namespace MongoRepository;

public class CarrinhoDeComprasRepository : BaseRepository<CarrinhoDeComprasDAO>, ICarrinhoDeComprasRepository
{
    public CarrinhoDeComprasRepository(AppDbContext context) : base(context)
    {
    }

    public async Task Atualizar(CarrinhoDeComprasDAO carrinhoDeCompras)
    {
       await Upsert(_context.CarrinhoDeCompras, carrinhoDeCompras, c => c.IdCarrinhoDeCompras == carrinhoDeCompras.IdCarrinhoDeCompras);
    }

    public async Task<CarrinhoDeComprasDAO?> BuscarCarrinhoDeComprasPorCpf(string cpf)
    {
        return GetList(_context.CarrinhoDeCompras, c => c.CPF == cpf && c.Status == "EmAberto").FirstOrDefault();
    }

    public async Task<CarrinhoDeComprasDAO?> BuscarCarrinhoDeComprasPorIdCarrinhoDeCompras(string idCarrinhoDeCompras)
    {
        return GetList(_context.CarrinhoDeCompras, c => c.IdCarrinhoDeCompras == idCarrinhoDeCompras && c.Status == "EmAberto").FirstOrDefault();
    }
}
