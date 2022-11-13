using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComercioElectronico.Domain.Model;

public class ShoppingCartItem
{
    public ShoppingCartItem()
    {

    }

    [Key]
    public Guid Id { get; set; }
    public int Quantity { get; set; } //cantidad
    [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Número máximo de dígitos")]

    public decimal Value { get; set; }
    [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Número máximo de dígitos")]

    public decimal TotalValue { get; set; }
    public string? Comment { get; set; }
    public Guid ProductId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    public Guid ShopingCartId { get; set; }

    [ForeignKey("ShopingCartId")]
    public virtual ShoppingCart ShoppingCart { get; set; }

}
