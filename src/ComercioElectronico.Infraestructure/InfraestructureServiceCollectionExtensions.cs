using System.Diagnostics;
using ComercioElectronico.Domain;
using ComercioElectronico.Domain.Repository;
using ComercioElectronico.Infraestructure.Controller;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComercioElectronico.Infraestructure;

public static class InfraestructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration config)
    {

        services.AddTransient<IBrandRepository, BrandRepository>();
        services.AddTransient<ITypeProductRepository, TypeProductRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IClientRepository, ClientRepository>();

        //Configurar DBContext
        services.AddDbContext<ECommerceDbContext>(options =>
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, config.GetConnectionString("ComercioElectronico"));
            Debug.WriteLine($"dbPath: {dbPath}");
            Console.WriteLine($"dbPath: {dbPath}");

            //Utilizar la base de datos sqlite
            options.UseSqlite($"Data Source={dbPath}");
        });

        //Utilizar una factoria
        services.AddScoped<IUnitOfWork>(provider =>
        {
            var instance = provider.GetService<ECommerceDbContext>();
            return instance;
        });



        return services;

    }
}
