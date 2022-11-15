using ComercioElectronico.Domain.Model;
using ComercioElectronico.Domain.Repository;
using ComercioElectronico.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;

//paso 7
namespace ComercioElectronico.Infraestructure.Controller;

public class ClientRepository : EfRepository<Client, Guid>, IClientRepository
{
    private readonly ECommerceDbContext _context;

    public ClientRepository(ECommerceDbContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<bool> ExistsIdAsync(Guid id)
    {
        var resultado = await this._context.Set<Client>()
                               .AnyAsync(x => x.Id == id);

        return resultado;


    }

    public async Task<bool> ExistsIdentificationAsync(string name)
    {
        var resultado = await this._context.Set<Client>()
                       .AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

        return resultado;
    }

    public async Task<Client> SearchClient(string search)
    {
        var resultado = this._context.Set<Client>()
                               .Where(x => (x.Identification.Contains(search) || x.Id.ToString().Contains(search)))
                               .SingleOrDefault();

        return resultado;
    }
}