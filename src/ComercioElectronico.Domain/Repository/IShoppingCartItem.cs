using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface IShoppingCartItemRepository : IRepository<ShoppingCartItem, Guid>
{
    public Task<bool> ExistsNameAsync(string name);

}