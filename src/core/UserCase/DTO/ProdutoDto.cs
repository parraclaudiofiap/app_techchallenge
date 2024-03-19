using Domain.ValueObjects;

namespace UserCase;

public class ProdutoDto
{
    public string Nome { get;  set; }

    public string Descricao { get;  set; }

    public CategoriaProdutoEnum Categoria { get;  set; }
    

    public double Preco { get;  set; }

    public string Imagem { get;  set; }
}
