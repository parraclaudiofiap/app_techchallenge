using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserCase;
using UserCase.Interfaces;
using WebApi.Controllers.CarrinhoDeCompras.Request;
using WebApi.Controllers.CarrinhoDeCompras.Response;

namespace WebApi.Controllers.CarrinhoDeCompras;

/// <summary>
/// O carrinho de compras é onde os clientes podem navegar e salvar itens que estão considerando comprar. 
/// </summary>
[ApiController]
[Route("carrinhodecompras")]
[Produces("application/json")]
public class CarrinhoDeComprasController : ControllerBase
{
    private readonly ICarrinhoDeComprasUserCase _carrinhoDeComprasUserCase;
    private readonly IMapper _mapper;

    /// <summary>
    /// AAA
    /// </summary>
    /// <param name="carrinhoDeCompras"></param>
    /// <param name="mapper"></param>
    public CarrinhoDeComprasController(ICarrinhoDeComprasUserCase carrinhoDeComprasUserCase, IMapper mapper)
    {
        _carrinhoDeComprasUserCase = carrinhoDeComprasUserCase;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Adicionar produto
    /// </summary>
    /// <returns>Retorna o carrinho de compras</returns>
    /// <response code="200">Retorna o estado atual do carrinho de compras.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CarrinhoDeComprasResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AdicionarProduto(CarrinhoDeComprasRequest request)
    {
        try
        {
            var carrinhoDeCompras = await _carrinhoDeComprasUserCase.AdicionarProduto(request.IdAtendimento, request.NomeProduto, request.CPF);
            
            return Ok(_mapper.Map<CarrinhoDeComprasResponse>(carrinhoDeCompras));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }

    
    /// <summary>
    /// Pesquisar carrinho de compras por CPF, com o status EmAberto
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Retorna o pedido criado</returns>
    /// <response code="200">Retorna o pedido criado.</response>
    /// /// <response code="204">Retorna qunando não obteve dados na consulta.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpGet("pesquisarporcpf/{cpf}")]
    [ProducesResponseType(typeof(CheckoutResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BuscaCarinhoDeComprasEmAbertoPorCPF([FromRoute] string cpf = "58669754088")
    {
        try
        {
            var carrinhoDeCompras = await _carrinhoDeComprasUserCase.BuscarCarrinhoDeComprasPorCpf(cpf);
            
            return carrinhoDeCompras is null 
                ? NoContent() 
                : Ok( _mapper.Map<CarrinhoDeComprasResponse>(carrinhoDeCompras));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }
    
    /// <summary>
    /// Executar checkout
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Retorna o pedido criado</returns>
    /// <response code="200">Retorna o pedido criado.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [ProducesResponseType(typeof(CheckoutResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    [Route("checkout")]
    public async Task<IActionResult>  Checkout(CheckoutRequest request)
    {
        try
        {
            var carrinhoDeCompras = await  _carrinhoDeComprasUserCase.ExecutarCheckout(request.idCarrinhoDeCompras);

            return Ok( _mapper.Map<CheckoutResponse>(carrinhoDeCompras));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }
}