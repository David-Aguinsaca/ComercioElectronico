//paso 4

namespace ComercioElectronico.Domain;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

}
