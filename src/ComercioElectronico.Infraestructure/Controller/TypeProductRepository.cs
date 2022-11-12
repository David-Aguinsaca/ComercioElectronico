using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using ComercioElectronico.Infraestructure.Repository;

namespace ComercioElectronico.Infraestructure.Controller;

public class TypeProductRepository : EfRepository<TypeProduct>, ITypeProductRepository
{
    private readonly ECommerceDbContext context;

    public TypeProductRepository(ECommerceDbContext context) : base(context)
    {
        this.context = context;
    }

    public Task<bool> ExistsNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
