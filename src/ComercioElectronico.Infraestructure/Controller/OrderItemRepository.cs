using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using ComercioElectronico.Infraestructure.Repository;

namespace ComercioElectronico.Infraestructure.Controller;

public class OrderItemRepository : EfRepository<OrderItem, Guid>, IOrderItemRepository
{
    private readonly ECommerceDbContext context;

    public OrderItemRepository(ECommerceDbContext context) : base(context)
    {
        this.context = context;
    }

    public Task<bool> ExistsNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
