using ComercioElectronico.Domain;

namespace ComercioElectronico.Application.Model;


public class OrderDto
{
    public Guid Id { get; set; }   
    public decimal TotalIva { get; set; }
    public decimal SubTotalValue { get; set; }
    public decimal TotalValue { get; set; }
    public virtual ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    public StatusShoppingCart StatusShoppingCart { get; set; }
    public Guid ClientId { get; set; }
    public virtual ClientDto Client { get; set; }

}