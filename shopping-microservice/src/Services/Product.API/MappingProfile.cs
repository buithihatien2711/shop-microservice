using AutoMapper;
using Product.API.Entities;
using Shared.DTOs.Product;
using Infrastructure.Mappings;

namespace Product.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductEntity, ProductDto>();
            
            CreateMap<CreateProductDto, ProductEntity>();

            CreateMap<UpdateProductDto, ProductEntity>()
                .IgnoreAllNonExsiting();
        }
    }
}
