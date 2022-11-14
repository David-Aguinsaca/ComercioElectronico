using ComercioElectronico.Domain;

namespace ComercioElectronico.Application.Model;


public class OrderCreateUpdateDto
{
    public StatusShoppingCart StatusShoppingCart { get; set; }
    public Guid ClientId { get; set; }

}