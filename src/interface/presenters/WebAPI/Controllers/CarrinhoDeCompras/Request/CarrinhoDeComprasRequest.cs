using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.CarrinhoDeCompras.Request;


public class CarrinhoDeComprasRequest
{
    /// <summary>
    /// Identificação da origem do pedido. Ex: Autoatendimento, Site Web, Atendendente
    /// </summary>
    [Required]
    [DefaultValue("Totem01")]
    public string IdAtendimento { get; set; }
    
    /// <summary>
    /// Nome de identificação do produto cadastrado.
    /// </summary>
    [Required]
    [DefaultValue("X-Bacon")]
    public string NomeProduto { get; set; }
    
    /// <summary>
    /// Identificação do cliente 
    /// </summary>
    [DefaultValue("58669754088")]
    public string? CPF { get; set; }
}