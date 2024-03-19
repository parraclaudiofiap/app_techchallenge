using UserCase;

namespace MeioPagamentoGateway;

public class MeioPagamentoGateway : IMeioPagamentoGateway
{
    public string GerarPagamentoQRCode()
    {
       return Guid.NewGuid().ToString();
    }
}
