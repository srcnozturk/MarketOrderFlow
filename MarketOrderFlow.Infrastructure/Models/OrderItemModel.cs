namespace MarketOrderFlow.Infrastructure.Models;

public class OrderItemModel : BaseModel
{
    public int Quantity { get; set; }

    // Hangi üründen sipariş verildiğini tutar
    public int ProductId { get; set; }
    public ProductModel Product { get; set; }

}
