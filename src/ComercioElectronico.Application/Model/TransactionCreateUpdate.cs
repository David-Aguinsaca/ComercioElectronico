
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ComercioElectronico.Application.Model;

public class TransactionCreateUpdateDto
{
    public string PaymentMethod { get; set; }
    public DateTime DateTransaction { get; set; }
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    //public ClientDto ClientDto { get; set; }
}