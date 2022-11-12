using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface IProductRepository : IRepository<Product>
{
    public Task<bool> ExistsNameAsync(string name);

}