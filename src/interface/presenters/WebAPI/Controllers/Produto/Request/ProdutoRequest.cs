using System.ComponentModel;
using Domain.ValueObjects;

namespace WebApi.Controllers.Produto;

public class ProdutoRequest
{
    /// <summary>
    /// Nome do produto ex: X-Bacon , X-Frango
    /// </summary>
    [DefaultValue("X-Egg")]
    public string Nome { get;  set; }
    /// <summary>
    /// Texto livre para descrição do produto
    /// </summary>
    [DefaultValue("Essa receita de hambúrguer com queijo e salada")]
    public string Descricao { get;  set; }
    /// <summary>
    /// Grupo no qual o produto pertence
    /// </summary>
    [DefaultValue(CategoriaProdutoEnum.Lanche)]
    public CategoriaProdutoEnum Categoria { get;  set; }
    
    /// <summary>
    /// Valor de venda do produto 
    /// </summary>
    [DefaultValue(15.90)]
    public double Preco { get;  set; }
    /// <summary>
    /// Imagem do produto
    /// </summary>
    [DefaultValue("x-egg")]
    public string Imagem { get;  set; }
}
