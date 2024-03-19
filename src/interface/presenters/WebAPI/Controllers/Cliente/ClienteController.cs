
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserCase;
using UserCase.DTO;
using UserCase.Interfaces;

namespace WebApi.Controllers.Cliente;

/// <summary>
/// Serviços disponiveis no contexto do agregado cliente
/// </summary>
[ApiController]
[Route("cliente")]
[Produces("application/json")]
public class ClienteController(IMapper mapper, IClienteUserCase clienteUserCase) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly IClienteUserCase _clienteUserCase = clienteUserCase;

    
    /// <summary>
    /// Cadastrar novo cliente
    /// </summary>
    /// <returns>Retorna o cliente cadastrado</returns>
    /// <response code="200">Retorna dados do cliente.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpPost("cadastrar")]
    [ProducesResponseType(typeof(ClienteResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarCliente(ClienteRequest clienteRequest)
    {
        try
        {
            var clienteDto = await _clienteUserCase.Cadastrar(_mapper.Map<ClienteDto>(clienteRequest));
            
            return Ok(_mapper.Map<ClienteResponse>(clienteDto));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }

    /// <summary>
    /// Identificar cliente por CPF
    /// </summary>
    /// <returns>Retorna dados do cliente cadastrado</returns>
    /// <response code="200">Retorna dados do cliente.</response>
    /// /// <response code="204">Retorna qunando não obteve dados na consulta.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpGet("pesquisarporcpf/{cpf}")]
    [ProducesResponseType(typeof(ClienteResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize]
    public async Task<IActionResult> BuscarClientePorCPF([FromRoute] string cpf = "58669754088")
    {
        try
        {
            var clienteDto = await _clienteUserCase.PesquisarPorCpf(cpf);
 
            return clienteDto is null 
                ? NoContent()
                : Ok(_mapper.Map<ClienteResponse>(clienteDto));
        }
        catch (Exception e)
        {   
            return BadRequest(new ErrorResponse(e.Message));
        }
    }
}