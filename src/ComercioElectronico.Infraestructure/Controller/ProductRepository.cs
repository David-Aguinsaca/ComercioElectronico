using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using ComercioElectronico.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;

//paso 7
namespace ComercioElectronico.Infraestructure.Controller;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    private readonly ECommerceDbContext _context;

    public ProductRepository(ECommerceDbContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<bool> ExistsNameAsync(string name)
    {
        var resultado = await this._context.Set<Product>()
                       .AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

        return resultado;
    }
}