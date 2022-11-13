using AutoMapper;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Application.Mapper;

public class ConfigurationMapperProfile : Profile
{
    public ConfigurationMapperProfile()
    {
        //Client
        CreateMap<Client, ClientDto>();
        CreateMap<ClientDto, Client>();
        CreateMap<Client, ClientCreateUpdateDto>();
        CreateMap<ClientCreateUpdateDto, Client>();
        CreateMap<Client, IEnumerable<ClientDto>>();

        //Product
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductCreateUpdateDto>();
        CreateMap<ProductCreateUpdateDto, Product>();
        CreateMap<Product, IEnumerable<ProductDto>>();


        //TypeProduct
        CreateMap<TypeProduct, TypeProductDto>();
        CreateMap<TypeProduct, TypeProductCreateUpdateDto>();
        CreateMap<TypeProductCreateUpdateDto, TypeProduct>();
        CreateMap<TypeProduct, IEnumerable<TypeProductDto>>();

        //brand
        CreateMap<Brand, BrandCreateUpdateDto>();
        CreateMap<BrandCreateUpdateDto, Brand>();

        CreateMap<Brand, BrandDto>();
        //CreateMap<BrandDto, Brand>();

        CreateMap<Brand, IEnumerable<BrandDto>>();
        //.ForMember(d => d.Select(d=>d.Id), op => op.Ignore());
        /* .ForMember(d => d.Select(d => new BrandDto{
            Name = d.Name,
            Branding = d.Branding
        })); */
        /* .ConvertUsing(src => src.CarIds?.Select(id => new EngineModelDto
        {
            Name = src.Name,
            Hp = src.Hp,
            CarId = id,
        })); */




    }
}
