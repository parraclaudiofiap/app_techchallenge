using UserCase.DTO;
using AutoMapper;
using WebApi.Controllers.Cliente;
using WebApi.Controllers.Produto;
using UserCase;
using WebApi.Controllers.CarrinhoDeCompras.Response;
using WebAPI;

namespace WebApi.AutoMapperConfig;

public class MapperProfiles : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public MapperProfiles()
    {
       CreateMap<ClienteRequest, ClienteDto>();
       CreateMap<ClienteDto, ClienteResponse>();
       CreateMap<ProdutoRequest, ProdutoDto>();
       CreateMap<ProdutoPutRequest, ProdutoDto>();
       CreateMap<ProdutoDto, ProdutoResponse>();       
       CreateMap<CarrinhoDeComprasDto, CarrinhoDeComprasResponse>();
       CreateMap<CarrinhoDeComprasDto, CheckoutResponse>();
       CreateMap<PedidoDTO, PedidoResponse>();
       CreateMap<FilaPedidosDTO, FilaPedidosData>();
       CreateMap<OrdemPagamentoDTO, OrdemPagamentoResponse>();
    }
}