using AutoMapper;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserCase;
using UserCase.Interfaces;

namespace WebApi.Controllers.Produto;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("produto")]
[Produces("application/json")]
[Authorize]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoUserCase _produtoUserCase;
    private readonly IMapper _mapper;

    public ProdutoController(IProdutoUserCase produtoUserCase, IMapper mapper)
    {
        _produtoUserCase = produtoUserCase;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Cadastrar Produto
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Retorna o produto criado</returns>
    /// <response code="200">Retorna o produto criado.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpPost("cadastrar")]
    [ProducesResponseType(typeof(ProdutoResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public  async Task<IActionResult> CadastrarProduto(ProdutoRequest produtoRequest)
    {
        try
        {
            var produto = _mapper.Map<ProdutoDto>(produtoRequest);
            
            await  _produtoUserCase.CadastrarProduto(produto);

            return Ok(_mapper.Map<ProdutoResponse>(produto));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }
    
    /// <summary>
    /// Editar produto
    /// </summary>
    /// <returns>Retorna o produto editado</returns>
    /// <response code="200">Retorna o produto editado.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpPut("editar")]
    [ProducesResponseType(typeof(ProdutoResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public  async Task<IActionResult> AtualizarProduto(ProdutoPutRequest produtoRequest)
    {
        try
        {
            var produto = _mapper.Map<ProdutoDto>(produtoRequest);
            
            await _produtoUserCase.EditarProduto(produto);

            return Ok(_mapper.Map<ProdutoResponse>(produto));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }
    
    
    /// <summary>
    /// Remover Produto
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Remove um produto cadastrado</returns>
    /// <response code="200">Retorna quando há sucesso na operaçõa</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpDelete("remover")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public  async Task<IActionResult> RemoverProduto(string name = "X-Egg")
    {
        try
        {
           await _produtoUserCase.RemoverProduto(name);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }
    
    /// <summary>
    /// Buscar produto por categoria
    /// </summary>
    /// <param name="item"></param>
    /// <returns>Retorna os produtos pesquisados</returns>
    /// <response code="200">Retorna o resultado da pesquisa</response>
    /// <response code="204">Retorna qunando não obteve dados na consulta.</response>
    /// <response code="400">Retorna Mensagem de Erro, gerado quando um fluxo de exceção ocorreu.</response>
    [HttpGet("")]
    [ProducesResponseType(typeof(ProdutosResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BuscarPorCategoria(CategoriaProdutoEnum categoria = CategoriaProdutoEnum.Lanche)
    {
        try
        {
            var dbProdutos = await _produtoUserCase.PesquisarProdutosPorCategoria(categoria);

            if (!dbProdutos.Any())
                return NoContent();

            var map = _mapper.Map<IList<ProdutoDto>, IList<ProdutoResponse>>(dbProdutos);
            
            return Ok(new ProdutosResponse(map));
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponse(e.Message));
        }
    }
}