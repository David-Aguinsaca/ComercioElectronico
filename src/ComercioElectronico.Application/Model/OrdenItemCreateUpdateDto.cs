namespace ComercioElectronico.Application.Model;

public class OrderItemCreateUpdateDto
{

    public int Quantity { get; set; } //cantidad
    //public decimal Value { get; set; }
    //public decimal TotalValue { get; set; }
    public string? Comment { get; set; }
    public Guid ProductId { get; set; }
    //public virtual ProductDto Product { get; set; }
    public Guid OrderId { get; set; }
    //public virtual ShoppingCartDto ShoppingCart { get; set; }

}