
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ComercioElectronico.Application.Model;

public class TransactionDto
{
    public Guid Id {get; set;}
    [Required]
    public string PaymentMethod { get; set; }
    public DateTime DateTransaction { get; set; }
    public Guid OrderId { get; set; }
    [ForeignKey("OrderId")]
    public OrderDto Order { get; set; }
    public Guid ClientId { get; set; }
    [ForeignKey("ClientId")]
    public ClientDto Client { get; set; }
}