using CloudGateway;
using Domain.Entities;
using UserCase.Interfaces.Gateways;

namespace Gateway;

public class ClienteGateway : IClienteGateway
{
    private IClienteRepository _clienteRepository;
    private IAuthGateway _authGateway;

    public ClienteGateway(IClienteRepository clienteRepository, IAuthGateway authGateway)
    {
        _clienteRepository = clienteRepository;
        _authGateway = authGateway;
    }

    public async Task<Cliente> BuscarPorCPF(string cpf)
    {
        var dbCliente = await _clienteRepository.BuscarPorCPF(cpf);

        return new Cliente(dbCliente.CPF, dbCliente.Nome, dbCliente.Email);
    }

    public async Task<bool> Inserir(Cliente cliente)
    {
       var authId = await _authGateway.CadastrarUsuario(cliente.CPF, "Test@@123");

        var clienteDAO = new ClienteDAO()
        {
            AuthId = authId,
            CPF = cliente.CPF,
            Nome = cliente.Nome,
            Email = cliente.Email
        };
        

       return await _clienteRepository.Inserir(clienteDAO);
    }

    public async Task<bool> UnicoCPF(string cpf)
    {
        return await _clienteRepository.UnicoCPF(cpf);
    }
}
