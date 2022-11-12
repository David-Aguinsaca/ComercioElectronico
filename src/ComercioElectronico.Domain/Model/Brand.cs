//paso 1

using System.ComponentModel.DataAnnotations;

namespace ComercioElectronico.Domain.Model;

//marca
public class Brand
{
    [Required]
    [Key]
    public int Id {get; set;}
    public string Name {get; set;}
    public string Branding {get; set;}
    public ICollection<Product> ListProduct{get; set;}
}
