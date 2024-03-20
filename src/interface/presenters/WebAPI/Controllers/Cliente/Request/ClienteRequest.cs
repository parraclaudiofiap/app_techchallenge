using System.ComponentModel;
namespace WebApi.Controllers.Cliente;

public class ClienteRequest
{
    /// <summary>
    /// O CPF é o Cadastro de Pessoa Física. Ele é um documento feito pela Receita Federal e serve para identificar os contribuintes.
    /// O CPF é uma numeração com 11 dígitos, que só mudam por decisão judicial.
    /// </summary>
    [DefaultValue("58669754088")]
    public string CPF { get;  set; }
    
    /// <summary>
    /// Nome do cliente
    /// </summary>
    [DefaultValue("Western Cape")]
    public string Nome { get;  set; }
    
    /// <summary>
    /// Email do cliente 
    /// </summary>
    [DefaultValue("WesternCape@hotmail.com")]
    public string Email{ get;  set; }

    public string Senha{ get;  set; }
}
