using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface ITypeProductRepository : IRepository<TypeProduct, int>
{
    public Task<bool> ExistsNameAsync(string name);

}
