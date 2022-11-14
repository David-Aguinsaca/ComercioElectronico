using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface IOrderItemRepository : IRepository<OrderItem, Guid>
{
    public Task<bool> ExistsNameAsync(string name);

}
