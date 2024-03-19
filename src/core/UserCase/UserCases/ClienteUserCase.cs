using Domain.Entities;
using UserCase.Interfaces;
using UserCase.Interfaces.Gateways;
using UserCase.DTO;
using AutoMapper;

namespace UserCase.UserCases;

public class ClienteUserCase : IClienteUserCase
{
    private readonly IClienteGateway _clienteGateway;

    private readonly IMapper _mapper;

    public ClienteUserCase(IMapper mapper, IClienteGateway clienteGateway)
    {
        _clienteGateway = clienteGateway;
        _mapper = mapper;
    }

    public async Task<ClienteDto> Cadastrar(ClienteDto clienteDto)
    {
        if (await _clienteGateway.UnicoCPF(clienteDto.CPF))
        {
            throw new InvalidOperationException("Cliente ja registrado !");
        }

         if(!await _clienteGateway.Inserir(_mapper.Map<Cliente>(clienteDto)))
               throw new InvalidOperationException("Erro na infraestrutura de persistencia");
         
         return clienteDto;
    }

    public async Task<ClienteDto> PesquisarPorCpf(string cpf)
    {
        var dbCliente = await _clienteGateway.BuscarPorCPF(cpf);

        return _mapper.Map<ClienteDto>(dbCliente);
    }
}