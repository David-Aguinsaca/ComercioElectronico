namespace ComercioElectronico.Application.Model;

public class TypeProductDto
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string? Description {get; set;}
    public string? Classification {get; set;}
    public decimal Discount {get; set;}
}
