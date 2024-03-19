namespace DbGateway;

public class ProdutoDAO : BaseDAO
{
    public string Nome { get;  set; }
    public string Descricao { get;  set; }
    public string Categoria { get;  set; }
    public double Preco { get;  set; }
    public string Imagem { get;  set; }
}
