
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserCase;
using UserCase.Interfaces;

namespace WebAPI;
/// <summary>
/// 
/// </summary>
[ApiController]
[Route("pedido")]
[Produces("application/json")]
[Authorize]
public class PedidoController : ControllerBase
{
    private readonly IPedidoUserCase _pedidoService;
    private readonly IMapper _mapper;

    public PedidoController(IPedidoUserCase pedidoService, IMapper mapper)
    {
        _pedidoService = pedidoService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Buscar todos os pedidos
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Retorna o pedido criado</returns>
    /// <response code="200">Retorna o pedido criado.</response>
    /// /// <response code="204">Retorna qunando não obteve dados na consulta.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpGet("listartodos")]
    [ProducesResponseType(typeof(List<PedidoResponse>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ListarTodos()
    {
        try
        {
            var dbFilaPedidos = await _pedidoService.BuscarTodos();
            
            if (dbFilaPedidos.Count > 0)
            {
                return Ok(dbFilaPedidos);
            }
            
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }
    
    /// <summary>
    /// Acompanhar pedidos
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Retorna o pedido criado</returns>
    /// <response code="200">Retorna o pedido criado.</response>
    /// /// <response code="204">Retorna qunando não obteve dados na consulta.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpGet("monitoramento")]
    [ProducesResponseType(typeof(FilaPedidosResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult>  Monitoramento()
    {
        try
        {
            var dbFilaPedidos =  await _pedidoService.BuscarTodosNaFila();
            
            if (dbFilaPedidos.Count > 0)
            {
                var filaPedidosResponse = new FilaPedidosResponse(_mapper.Map<List<FilaPedidosData>>(dbFilaPedidos));
                return Ok( filaPedidosResponse);
            }
            
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }


    /// <summary>
    /// Atualiza o progresso do pedido
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Retorna o pedido atualizado</returns>
    /// <response code="200">Retorna o pedido atualizado.</response>
    /// /// <response code="204">Retorna qunando não obteve dados na consulta.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpPut("atualizarprogresso")]
    [ProducesResponseType(typeof(PedidoResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Atualizar(string numeroPedido = "8767", string progressoPedido = "EmPreparacao")
    {
        try
        {
            var pedidoDTO = await _pedidoService.AtualizarProgressoPedido(numeroPedido,progressoPedido);
            
            return Ok(_mapper.Map<PedidoResponse>(pedidoDTO));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }
}