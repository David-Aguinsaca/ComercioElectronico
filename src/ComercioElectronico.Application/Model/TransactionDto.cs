
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ComercioElectronico.Application.Model;

public class TransactionDto
{
    public Guid Id {get; set;}
    public string PaymentMethod { get; set; }
    public DateTime DateTransaction { get; set; }
    public Guid OrderId { get; set; }
    public OrderDto Order { get; set; }
    public Guid ClientId { get; set; }
    public ClientDto Client { get; set; }
}