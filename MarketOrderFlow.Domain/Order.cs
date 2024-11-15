namespace MarketOrderFlow.Domain;

/// <summary>
/// Aggregate Root
/// Sipariş
/// </summary>
public class Order
{
    public int Id { get; private set; }
    public DateTime OrderDate { get; private set; }
    public int MarketId { get; private set; }
    public Market Market { get; private set; }

    // Sipariş edilen ürünlerin listesi
    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    // Constructor
    public Order(int marketId)
    {
        MarketId = marketId;
        OrderDate = DateTime.UtcNow;
    }

    // Siparişe ürün ekleyebilmek için bir metod
    public void AddOrderItem(Product product, int quantity)
    {
        var orderItem = new OrderItem(product, quantity);
        OrderItems.Add(orderItem);
    }
    //public void ApproveOrder(int quantity)
    //{
    //    if (quantity < SuggestedQuantity)
    //        throw new InvalidOperationException("Approved quantity cannot be lower than the suggested quantity.");

    //    ApprovedQuantity = quantity;
    //}
}
