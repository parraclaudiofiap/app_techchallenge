namespace WebApi.Controllers.Produto;

public class ProdutosResponse
{
    public ProdutosResponse(IEnumerable<ProdutoResponse> produtos)
    {
        Produtos = produtos;
    }
    /// <summary>
    /// Lista de produtos
    /// </summary>
    public IEnumerable<ProdutoResponse> Produtos { get; private set; }
}