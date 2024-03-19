using DbGateway;

namespace Gateway;

public class ClienteDAO : BaseDAO
{
    public string AuthId{ get;  set; }
    public string CPF { get;  set; }
    public string Nome { get;  set; }
    public string Email{ get;  set; }
}
