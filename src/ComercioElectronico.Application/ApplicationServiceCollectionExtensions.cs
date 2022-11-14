
using System.Reflection;
using ComercioElectronico.Application.Controller;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using FluentValidation;
//using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComercioElectronico.Application;

public static class ApplicationServiceCollectionExtensions
{

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {

        //Inyeccion dependencias AppService
        services.AddTransient<IAppService<BrandDto, BrandCreateUpdateDto, int>, BrandAppService>();
        services.AddTransient<IAppService<TypeProductDto, TypeProductCreateUpdateDto, int>, TypeProductAppService>();
        services.AddTransient<IAppService<ProductDto, ProductCreateUpdateDto, Guid>, ProductAppService>();
        services.AddTransient<IAppService<ClientDto, ClientCreateUpdateDto, Guid>, ClientAppService>();
        services.AddTransient<IAppService<ShoppingCartDto, ShoppingCartCreateUpdatetDto, Guid>, ShoppingCartAppService>();
        services.AddTransient<IAppService<ShoppingCartItemDto, ShoppingCartItemCreateUpdatetDto, Guid>, ShoppingCartItemAppService>();
        /* services.AddTransient<IProductAppService<ProductDto, ProductCreateUpdateDto>, ProductAppService>(); */

        //Configurar la inyección de todos los profile que existen en un Assembly
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //Configurar los validaciones
        /* services.AddScoped<IValidator<BrandCreateUpdateDto>, 
                        BrandCreateUpdateDtoValidator>(); */
 
        //Configurar todas las validaciones
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); 

        return services;

    }
}
