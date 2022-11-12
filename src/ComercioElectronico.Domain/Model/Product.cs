using System.ComponentModel.DataAnnotations;

namespace ComercioElectronico.Domain.Model;

public class Product
{
    [Required]
    [Key]
    public int Id {get; set;}
    public int Stock{get;set;}
    public string? Description {get; set;}
    public decimal Value{get;set;}
    public bool IsIva{get;set;}

    [Required]
    public int BrandId{get; set;}
    public virtual Brand Brand {get;set;}
    [Required]
    public int TypeProductId {get;set;}
    public virtual TypeProduct TypeProduct{get; set;}
    
}
