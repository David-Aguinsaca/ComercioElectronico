using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using ComercioElectronico.Infraestructure.Repository;

namespace ComercioElectronico.Infraestructure.Controller;

public class TransactionRepository : EfRepository<Transaction, Guid>, ITransactionRepository
{
    private readonly ECommerceDbContext context;

    public TransactionRepository(ECommerceDbContext context) : base(context)
    {
        this.context = context;
    }

    public Task<bool> ExistsNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
