using DbGateway;
using MongoRepository.Context;
using MongoRepository.Repositories;

namespace MongoRepository;

public class OrdemPagamentoRepository : BaseRepository<OrdemPagamentoDAO>, IOrdemPagamentoRepository
{
    public OrdemPagamentoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AtualizarOrdemPagamento(OrdemPagamentoDAO ordemDePagamento)
    {
        await Update(_context.OrdemPagamento, ordemDePagamento, o => o.Id == ordemDePagamento.Id);
    }

    public async Task<OrdemPagamentoDAO?> BuscarOrdemPagamentoPorId(string idOrdemPagamento)
    {
      return GetList(_context.OrdemPagamento, o => o.IdOrdemPagamento == idOrdemPagamento).FirstOrDefault();
    }

    public async Task SalvarOrdemPagamento(OrdemPagamentoDAO ordemDePagamento)
    {
        await InsertOne(_context.OrdemPagamento, ordemDePagamento);
    }
}
