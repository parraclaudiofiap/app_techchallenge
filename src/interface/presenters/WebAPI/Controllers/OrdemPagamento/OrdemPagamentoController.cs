using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserCase;

namespace WebAPI;

/// <summary>
/// Webhook para receber o callback do gateway de meio de pagamento
/// </summary>
[ApiController]
[Route("pagamento")]
[Produces("application/json")]
[Authorize]
public class OrdemPagamentoController(IOrdemPagamentoUserCase ordemPagamentoUserCase, IMapper mapper) : ControllerBase
{
    private readonly IOrdemPagamentoUserCase _ordemPagamentoUserCase = ordemPagamentoUserCase;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Callback do meio de pagamento
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Retorna OK</returns>
    /// <response code="200">Retorna sucesso.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    [Route("webhook/callback/{idPagamento}/{estadoPagamento}")]
    public async Task<IActionResult> CallbackPagamento(string idPagamento = "8767ac05-fbc3-487e-98f6-b1c6b88fa31c", string estadoPagamento = "Aprovado")
    {
        try
        {
            await _ordemPagamentoUserCase.AtualizarStatusPagamento(idPagamento, estadoPagamento);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }

    /// <summary>
    /// Pesquisar pagamento
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Retorna o pagamento criado</returns>
    /// <response code="200">Retorna o pagamento criado.</response>
    ///  <response code="204">Retorna quando não obteve dados na consulta.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpGet("pesquisar/{idPagamento}")]
    [ProducesResponseType(typeof(OrdemPagamentoResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BuscaPagamento([FromRoute] string idPagamento = "8767ac05-fbc3-487e-98f6-b1c6b88fa31c")
    {
        try
        {
            var ordemPagamentoDTO = await _ordemPagamentoUserCase.BuscarPagamento(idPagamento);
            return ordemPagamentoDTO is null 
                ? NoContent() 
                : Ok(_mapper.Map<OrdemPagamentoResponse>(ordemPagamentoDTO));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }
    

}


