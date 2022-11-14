
namespace ComercioElectronico.Domain.Model;

public class Transaction
{
    public Guid Id;
    public string PaymentMethod { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; }
}