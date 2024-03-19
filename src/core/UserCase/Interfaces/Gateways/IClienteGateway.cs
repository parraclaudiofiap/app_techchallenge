using Domain.Entities;

namespace UserCase.Interfaces.Gateways;

public interface IClienteGateway
{
    Task<bool> Inserir(Cliente cliente);
    
    Task<Cliente> BuscarPorCPF(string cpf);

    Task<bool> UnicoCPF(string cpf);
}