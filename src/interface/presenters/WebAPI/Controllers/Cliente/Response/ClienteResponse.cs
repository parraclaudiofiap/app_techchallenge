namespace WebApi.Controllers.Cliente;
/// <summary>
/// 
/// </summary>
public class ClienteResponse
{
    
    /// <summary>
    /// Identificação do Cliente
    /// </summary>
    public string CPF { get;  set; }
    /// <summary>
    /// Nome do Cliente
    /// </summary>
    public string Nome { get;  set; }
    /// <summary>
    /// Email do cliente
    /// </summary>
    public string Email{ get;  set; }
}