
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ComercioElectronico.Application.Model;

public class TransactionCreateUpdateDto
{
    [Required]
    public string PaymentMethod { get; set; }
    public DateTime DateTransaction { get; set; }
    [Required]
    public Guid OrderId { get; set; }
    [Required]
    public Guid ClientId { get; set; }
    public ClientDto ClientDto { get; set; }
}