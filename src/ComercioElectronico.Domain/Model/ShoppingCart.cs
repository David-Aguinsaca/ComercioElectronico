using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComercioElectronico.Domain.Model;

public class ShoppingCart
{
    public ShoppingCart()
    {
    }

    [Key]
    public Guid Id { get; set; }
    
    [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Número máximo de dígitos")]
    public decimal TotalIva { get; set; }

    [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Número máximo de dígitos")]
    public decimal SubTotalValue { get; set; }

    [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Número máximo de dígitos")]
    public decimal TotalValue { get; set; }
    
    public virtual ICollection<ShoppingCartItem> ShoppingCarItems { get; set; } = new List<ShoppingCartItem>();
    public StatusShoppingCart StatusShoppingCart { get; set; }
    public Guid ClientId { get; set; }
    [ForeignKey("ClientId")]
    public virtual Client Client { get; set; }

}
