using ComercioElectronico.Domain.Model;

namespace ComercioElectronico.Application.Model;

public class ProductDto
{
    public Guid Id { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public bool IsIva { get; set; }
    public int BrandId { get; set; }
    public int TypeProductId { get; set; }

    public BrandDto Brand {get; set;}
    public TypeProductDto TypeProduct {get; set;}

    /* public virtual ICollection<Brand> ListBrand {get;set;}
    public virtual ICollection<TypeProduct> TypeProducts {get;set;} */

}
