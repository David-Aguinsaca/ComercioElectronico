using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface ITypeProductRepository : IRepository<TypeProduct>
{
    public Task<bool> ExistsNameAsync(string name);

}
