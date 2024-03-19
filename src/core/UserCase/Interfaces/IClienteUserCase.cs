using Domain.Entities;
using UserCase.DTO;

namespace UserCase.Interfaces;

public interface IClienteUserCase 
{
    Task<ClienteDto> Cadastrar(ClienteDto cliente);
    
    Task<ClienteDto> PesquisarPorCpf(string cpf);
}