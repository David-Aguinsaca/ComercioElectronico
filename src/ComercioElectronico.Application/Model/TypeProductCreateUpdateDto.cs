namespace ComercioElectronico.Application.Model;

public class TypeProductCreateUpdateDto
{
    public string Name {get; set;}
    public string? Description {get; set;}
    public string? Classification {get; set;}
    public decimal Discount {get; set;}
}
