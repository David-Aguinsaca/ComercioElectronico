
using System.Reflection;
using ComercioElectronico.Application.Controller;
using ComercioElectronico.Application.Model;
using ComercioElectronico.Application.Repository;
using ComercioElectronico.Application.Validator;
using FluentValidation;
//using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComercioElectronico.Application;

public static class ApplicationServiceCollectionExtensions
{

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {

        //services.AddTransient<IBrandAppService, BrandAppService>();
        services.AddTransient<IAppService<BrandDto, BrandCreateUpdateDto>, BrandAppService>();

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
