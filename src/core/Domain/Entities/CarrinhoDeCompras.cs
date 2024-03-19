using Domain.ValueObjects;

namespace Domain.Entities;

public class CarrinhoDeCompras
{
    public CarrinhoDeCompras(string idAtendimento)
    {
        IdAtendimento = idAtendimento;
        IdCarrinhoDeCompras = Guid.NewGuid().ToString();
        Status = StatusCarrinhoDeCompras.EmAberto;
        Produtos = new List<Produto>();
    }
    
    public CarrinhoDeCompras(string idAtendimento, string cpf)
    {
        IdAtendimento = idAtendimento;
        IdCarrinhoDeCompras = Guid.NewGuid().ToString();
        CPF = cpf;
        Status = StatusCarrinhoDeCompras.EmAberto;
        Produtos = new List<Produto>();
    }
    
    public CarrinhoDeCompras(string idAtendimento, string idCarrinhoDeCompras, List<Produto> produtos, string? cpf = null, StatusCarrinhoDeCompras? statusCarrinhoDeCompras = StatusCarrinhoDeCompras.EmAberto)
    {
        IdAtendimento = idAtendimento;
        IdCarrinhoDeCompras = idCarrinhoDeCompras;
        CPF = cpf;
        Status = StatusCarrinhoDeCompras.EmAberto;
        Produtos = produtos;
    }

    public string IdAtendimento { get; private set; }
    public string IdCarrinhoDeCompras { get; private set; }
    
    public string? CPF { get; private set;}

    public StatusCarrinhoDeCompras Status { get; private set; }
    
    public List<Produto> Produtos { get; private set; }
    
    public void SetStatus(StatusCarrinhoDeCompras statusCarrinhoDeCompras)
    {
        Status = statusCarrinhoDeCompras;
    }

    public double SomaDoPreco => Produtos.Sum(p => p.Preco);
    
    public int QuantidadeItens => Produtos.Count;

}