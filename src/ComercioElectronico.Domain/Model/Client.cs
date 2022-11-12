using System.ComponentModel.DataAnnotations;

namespace ComercioElectronico.Domain.Model;

public class Client
{
    [Required]
    [Key]
    public int Id {get; set;}

    [StringLength(10)]
    [Required]
    public string Identification {get; set;}
    public string Name {get; set;}

    [RegularExpression(@"^[\w-\.] +@([\w-]+\.) +[\w-]{2,4}$", ErrorMessage ="Adresse mail non valide!" )]
    public string? Email {get; set;}

    [Required]
    public double Discount {get; set;}
}
