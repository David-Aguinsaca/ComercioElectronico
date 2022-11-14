using ComercioElectronico.Domain;

namespace ComercioElectronico.Application.Model;


public class ShoppingCartDto
{
    public Guid Id { get; set; }   
    public decimal TotalIva { get; set; }
    public decimal SubTotalValue { get; set; }
    public decimal TotalValue { get; set; }
    public virtual ICollection<ShoppingCartItemDto> ShoppingCarItems { get; set; } = new List<ShoppingCartItemDto>();
    public StatusShoppingCart StatusShoppingCart { get; set; }
    public Guid ClientId { get; set; }
    public virtual ClientDto Client { get; set; }

}
