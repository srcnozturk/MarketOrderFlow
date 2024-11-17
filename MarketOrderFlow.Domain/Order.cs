using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.Domain;

/// <summary>
/// Aggregate Root
/// Sipariş
/// </summary>
public class Order : IOrder
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public DateTime OrderDate { get; private set; }
    public IMarket Market { get; private set; }

    // Sipariş edilen ürünlerin listesi
    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    // Constructor
    public Order(long? id,string name,DateTime orderdate,IMarket market)
    {
        Id = id;
        Name = name;
        OrderDate = DateTime.UtcNow;
        Market = market;
    }

    public static async Task<IOrder> New(
           long? id,
           string name,
           DateTime orderDate,
           IMarket market = null)
    {
        Order order = new(
            id, name, orderDate, market);
        return order;
    }
    public void AddOrderItem(Product product, int quantity)
    {
        var orderItem = new OrderItem(product, quantity);
        OrderItems.Add(orderItem);
    }
}

