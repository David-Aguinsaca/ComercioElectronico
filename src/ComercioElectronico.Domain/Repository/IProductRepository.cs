using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface IProductRepository : IRepository<Product, Guid>
{
    public Task<bool> ExistsNameAsync(string name);

}