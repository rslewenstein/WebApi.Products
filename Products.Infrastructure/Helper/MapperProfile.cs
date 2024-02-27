using AutoMapper;
using Products.Domain;
using Products.Domain.Dtos;

namespace Products.Infrastructure.Helper
{
    public class MapperProfile: Profile 
    {
        public MapperProfile() {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductMessageDto>();
            CreateMap<ProductMessageDto, Product>();
        }
    }
}