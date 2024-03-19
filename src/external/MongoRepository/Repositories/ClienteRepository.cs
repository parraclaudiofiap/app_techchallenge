using Gateway;
using MongoDB.Driver;
using MongoRepository.Context;


namespace MongoRepository.Repositories;

public class ClienteRepository : BaseRepository<ClienteDAO>, IClienteRepository
{
    public ClienteRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<ClienteDAO?> BuscarPorCPF(string cpf)
    {
        try{
  
            var clienteDAO = GetList( _context.Clientes,  c => c.CPF == cpf);

            if (!clienteDAO.Any())
                return null;

            return clienteDAO.FirstOrDefault();
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> Inserir(ClienteDAO cliente)
    {            
        try
        {
            return await InsertOne(_context.Clientes, cliente);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> UnicoCPF(string cpf)
    {
        return GetList( _context.Clientes,  c => c.CPF == cpf).Any();
    }
}