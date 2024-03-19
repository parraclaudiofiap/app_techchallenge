namespace CloudGateway;

public interface IAuthGateway
{
    Task<string>  CadastrarUsuario(string username, string password);
}
