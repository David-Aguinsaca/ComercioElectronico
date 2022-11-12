//paso 5

using ComercioElectronico.Domain;
using ComercioElectronico.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace ComercioElectronico.Infraestructure;

public class ECommerceDbContext : DbContext, IUnitOfWork
{
    public DbSet<Brand> Brands { get; set; }

    public string DbPath { get; set; }

    /* public ECommerceDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "ecommerce.v1.db");

    } */

    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
    {
    }

    /* protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}") ;*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


    }
}
