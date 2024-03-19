using UserCase.DTO;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace UserCase.Mapping;

public class MapperProfiles : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public MapperProfiles()
    {
       CreateMap<ClienteDto, Cliente>()
        .ReverseMap()
        .ForMember(d => d.CPF , c => c.MapFrom(ci => ci.CPF._value));
        
        CreateMap<ProdutoDto, Produto>().ReverseMap();
        CreateMap<CarrinhoDeComprasDto, CarrinhoDeCompras>().ReverseMap();
        CreateMap<OrdemDePagamento, OrdemPagamentoDTO>();
        CreateMap<PedidoDTO, Pedido>().ReverseMap();
        CreateMap<FilaPedidosDTO, FilaPedidos>().ReverseMap();
    }
}