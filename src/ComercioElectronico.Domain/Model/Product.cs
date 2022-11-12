using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComercioElectronico.Domain.Model;

public class Product
{

    public Product(){

    }
    
    /* public Product(Guid id)
    {
        this.Id = id;
    } */

    [Required]
    [Key]
    public Guid Id { get; set; }
    public int Stock { get; set; }
    public string? Description { get; set; }
    [Required]
    public string Name { get; set; }
    public decimal Value { get; set; }
    public bool IsIva { get; set; }
    [Required]
    public int BrandId { get; set; }
    [Required]
    public int TypeProductId { get; set; }

    [ForeignKey("BrandId")]
    public virtual Brand Brand { get; set; }
    [ForeignKey("TypeProductId")]
    public virtual TypeProduct TypeProduct { get; set; }

}
