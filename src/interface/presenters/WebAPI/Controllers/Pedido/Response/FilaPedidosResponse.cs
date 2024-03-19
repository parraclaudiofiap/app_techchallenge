using Domain.Entities;

namespace WebAPI;

public class FilaPedidosResponse
{
    /// <summary>
    /// Fila de controle de atendimento dos pedidos
    /// </summary>
    public IList<FilaPedidosData> FilaPedidos { get; set; }

    public FilaPedidosResponse(List<FilaPedidosData>  filaPedidosDatas)
    {
        FilaPedidos = filaPedidosDatas;
    }
}