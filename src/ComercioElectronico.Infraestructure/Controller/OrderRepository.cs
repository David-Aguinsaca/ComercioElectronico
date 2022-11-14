using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using ComercioElectronico.Infraestructure.Repository;

namespace ComercioElectronico.Infraestructure.Controller;

public class OrderRepository : EfRepository<Order, Guid>, IOrderRepository
{
    private readonly ECommerceDbContext context;

    public OrderRepository(ECommerceDbContext context) : base(context)
    {
        this.context = context;
    }

    public Task<bool> ExistsNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
