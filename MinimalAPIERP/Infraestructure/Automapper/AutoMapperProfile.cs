using AutoMapper;
using ERP;
using MinimalAPIERP.Dtos;
using SistemaVentaBlazor.Server.Models;

namespace MinimalAPIERP.Infraestructure.Automapper 
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
                        
            CreateMap<Producto, ProductoDTO>()
            .ForMember(destino =>
                destino.IdCategoria,
                opt => opt.Ignore()
            );
            
        }
    }

}

