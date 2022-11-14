using ComercioElectronico.Domain;

namespace ComercioElectronico.Application.Model;


public class ShoppingCartCreateUpdatetDto
{
    //public virtual ICollection<ShoppingCartItemDto> ShoppingCarItems { get; set; } = new List<ShoppingCartItemDto>();
    public StatusShoppingCart StatusShoppingCart { get; set; }
    public Guid ClientId { get; set; }

}
