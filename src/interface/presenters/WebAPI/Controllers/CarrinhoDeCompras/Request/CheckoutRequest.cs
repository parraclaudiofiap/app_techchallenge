using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.CarrinhoDeCompras.Request;

public class CheckoutRequest
{
    /// <summary>
    /// Identificação do carrinho de compras
    /// </summary>
    [Required]
    [DefaultValue("8767ac05-fbc3-487e-98f6-b1c6b88fa31c")]
    public string idCarrinhoDeCompras { get; set; }
}