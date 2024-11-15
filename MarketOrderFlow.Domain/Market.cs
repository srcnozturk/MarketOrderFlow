namespace MarketOrderFlow.Domain;

/// <summary>
/// Entity
/// Market sınıfı
/// </summary>
public sealed class Market 
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    // Market'in bağlı olduğu lojistik merkezinin bilgisi
    public int LogisticsCenterId { get; private set; }
    public LogisticsCenter LogisticsCenter { get; private set; }

    // Market'e ait siparişleri tutar
    public ICollection<Order> Orders { get; private set; } = new List<Order>();

    // Constructor
    public Market(string name, int logisticsCenterId)
    {
        Name = name;
        LogisticsCenterId = logisticsCenterId;
    }

    //public void PlaceOrder(int productCode, OrderQuantity orderQuantity)
    //{
    //    if (!LogisticsCenter.HasProductInStock(productCode))
    //        throw new InvalidOperationException("Cannot order a product not available in the logistics center.");

    //    var order = new Order(productCode, orderQuantity.Quantity);
    //    _orders.Add(order);
    //}
}
