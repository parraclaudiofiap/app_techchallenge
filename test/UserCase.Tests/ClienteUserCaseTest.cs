using UserCase.UserCases;
using UserCase.Interfaces.Gateways;
using UserCase.Interfaces;
using UserCase.DTO;
using Domain.Entities;
using Moq;
using AutoMapper;
using UserCase.Mapping;

namespace UserCase.Tests;

public class ClienteUserCaseTest
{
    private IClienteUserCase _clienteUserCase;
    private Moq.Mock<IClienteGateway> _clienteGatewayMock;

    public ClienteUserCaseTest()
    {
        //auto mapper configuration
        var mockMapper = new MapperConfiguration(cfg =>
        {
            //cfg.ShouldMapProperty = p=> p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile(new MapperProfiles());
        });

        var mapperMock = mockMapper.CreateMapper();
        _clienteGatewayMock = new Moq.Mock<IClienteGateway>();
        _clienteUserCase = new ClienteUserCase(mapperMock, _clienteGatewayMock.Object);
    }

    [Fact]
    public async void CadastrarComSucesso()
    {
        _clienteGatewayMock.Setup(g => g.UnicoCPF(It.IsAny<string>())).Returns(Task.FromResult(false));
        _clienteGatewayMock.Setup(g => g.Inserir(It.IsAny<Cliente>())).Returns(Task.FromResult(true));

        var clienteDto = new ClienteDto()
        {
            CPF  =  "58669754088",
            Nome  = "Western Cape",
            Email = "WesternCape@hotmail.com"
        };

        var result = await _clienteUserCase.Cadastrar(clienteDto);

        Assert.NotNull(result);
    }

    [Fact]
    public async void PesquisarPorCpfSucesso()
    {
        var cliente = new Cliente("58669754088", "Western Cape","WesternCape@hotmail.com", null);

        _clienteGatewayMock.Setup(g => g.BuscarPorCPF(It.IsAny<string>())).Returns(Task.FromResult(cliente));

        var result = await _clienteUserCase.PesquisarPorCpf(cliente.CPF);

        Assert.NotNull(result);
        Assert.Equal(cliente.CPF, result.CPF);
        Assert.Equal(cliente.Nome, result.Nome);    
        Assert.Equal(cliente.Email, result.Email);
    }
}