namespace Gateway;

public interface IClienteRepository
{
    Task<bool> Inserir(ClienteDAO cliente);
    
    Task<ClienteDAO?> BuscarPorCPF(string cpf);

    Task<bool> UnicoCPF(string cpf);
}