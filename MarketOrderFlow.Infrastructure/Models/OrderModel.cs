namespace MarketOrderFlow.Infrastructure.Models;

public class OrderModel : BaseModel
{
    public DateTime OrderDate { get; set; }
    public MarketModel Market { get;  set; }

    // Sipariş edilen ürünlerin listesi
    public ICollection<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();
}
