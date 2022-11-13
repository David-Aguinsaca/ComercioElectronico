using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface IClientRepository : IRepository<Client, Guid>
{
    public Task<bool> ExistsIdentificationAsync(string identification);

}