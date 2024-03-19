using Domain.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Produto
{
    public Produto(string nome, string descricao, CategoriaProdutoEnum categoria, double preco, string imagem)
    {
        Nome = nome;
        Descricao = descricao;
        Categoria = categoria;
        Imagem = imagem;
        Preco = preco;
        ValidateEntity();
    }

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public CategoriaProdutoEnum Categoria { get; private set; }
    
    public double Preco { get; private set; }
    public string Imagem { get; private set; }

    private void ValidateEntity()
    {
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O Nome não pode estar em branco");
        AssertionConcern.AssertArgumentNotEmpty(Descricao, "O Nome não pode estar em branco");
        AssertionConcern.AssertArgumentNotNull(Categoria, "O Nome não pode estar em branco");
        
        //TODO: Criar validação para o preço  
    }
    
}