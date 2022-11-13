using System.ComponentModel.DataAnnotations;

namespace ComercioElectronico.Application.Model;

public class ClientDto
{

    public Guid Id { get; set; }

    [StringLength(10, ErrorMessage = "Minimo 10 digitos")]
    [Required]
    public string Identification { get; set; } //cedula, pasaporte
    public string Name { get; set; }
    public string? Phone { get; set; }

    [RegularExpression(@"^[\w-\.] +@([\w-]+\.) +[\w-]{2,4}$", ErrorMessage = "Correo no cumple con el formato")]
    public string? Email { get; set; }

    [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage ="Número máximo de dígitos")]
    public double Discount { get; set; }

}
