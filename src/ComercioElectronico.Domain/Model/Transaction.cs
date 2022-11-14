using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComercioElectronico.Domain.Model;

public class Transaction
{
    

    public Transaction()
    {
    }
    [Key]
    public Guid Id {get; set;}
    [Required]
    public string PaymentMethod { get; set; }
    public DateTime DateTransaction { get; set; }
    public Guid OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order Order { get; set; }
    public Guid ClientId { get; set; }
    [ForeignKey("ClientId")]
    public Client Client { get; set; }

}