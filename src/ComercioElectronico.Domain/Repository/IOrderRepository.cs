using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface IOrderRepository : IRepository<Order, Guid>
{
    public Task<bool> ExistsNameAsync(string name);

}
