
using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Domain.Repository;

public interface ITransactionRepository : IRepository<Transaction, Guid>
{
    public Task<bool> ExistsNameAsync(string name);

}
