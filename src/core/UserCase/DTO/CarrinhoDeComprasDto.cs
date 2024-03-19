using Domain.Entities;
using Domain.ValueObjects;

namespace UserCase;

public class CarrinhoDeComprasDto
{
    public string IdAtendimento { get; private set; }
    public string IdCarrinhoDeCompras { get; private set; }
    
    public bool ClienteIdentificado => (CPF is not null);
    
    public string? CPF { get; private set;}

    public StatusCarrinhoDeCompras Status { get; private set; }
    
    public List<ProdutoDto> Produtos { get; private set; }
    
    public string NumeroPedido { get; private set; }
    
    public double SomaDoPreco => Produtos.Sum(p => p.Preco);
    
    public int QuantidadeItens => Produtos.Count;

    public OrdemDePagamento OrdemDePagamento {get; set;}
}
