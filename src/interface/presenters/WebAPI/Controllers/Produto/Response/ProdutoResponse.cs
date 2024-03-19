using Domain.ValueObjects;

namespace WebApi.Controllers.Produto;

public class ProdutoResponse
{
    /// <summary>
    /// Nome do produto ex: X-Bacon , X-Frango
    /// </summary>
    public string Nome { get;  set; }
    /// <summary>
    /// Texto livre para descrição do produto
    /// </summary>
    public string Descricao { get;  set; }
    /// <summary>
    /// Grupo no qual o produto pertence
    /// </summary>
    public CategoriaProdutoEnum Categoria { get;  set; }
    /// <summary>
    /// Valor de venda do produto 
    /// </summary>
    public double Preco { get;  set; }
    /// <summary>
    /// Imagem do produto
    /// </summary>
    public string Imagem { get;  set; }
}