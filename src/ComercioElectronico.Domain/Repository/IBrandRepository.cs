//paso 3

using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface IBrandRepository : IRepository<Brand, int>
{
    public Task<bool> ExistsNameAsync(string name);

}
