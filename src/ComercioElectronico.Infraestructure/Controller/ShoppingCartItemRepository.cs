using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using ComercioElectronico.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;

//paso 7
namespace ComercioElectronico.Infraestructure.Controller;

public class ShoppingCartItemRepository : EfRepository<ShoppingCartItem, Guid>, IShoppingCartItemRepository
{
    private readonly ECommerceDbContext _context;

    public ShoppingCartItemRepository(ECommerceDbContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<bool> ExistsNameAsync(string name)
    {
        /* var resultado = await this._context.Set<ShoppingCartItem>()
                       .AnyAsync(x => x.ShoppingCarItems.ToUpper() == name.ToUpper()); */

        return false;
    }
}