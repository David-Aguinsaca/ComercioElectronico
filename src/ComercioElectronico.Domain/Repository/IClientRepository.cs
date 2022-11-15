using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface IClientRepository : IRepository<Client, Guid>
{
    public Task<bool> ExistsIdentificationAsync(string identification);

    public Task<bool> ExistsIdAsync(Guid id);

    public Task<Client> SearchClient(string search);

}