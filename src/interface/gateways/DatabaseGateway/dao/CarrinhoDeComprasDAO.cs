namespace DbGateway;

public class CarrinhoDeComprasDAO : BaseDAO
{
    public string IdAtendimento { get; set; }
    public string IdCarrinhoDeCompras { get; set; }
    
    public string CPF { get;  set;}

    public string Status { get;  set; }
    
    public List<ProdutoDAO> Produtos { get;  set; }
}
