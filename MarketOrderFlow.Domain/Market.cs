using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.Domain;

/// <summary>
/// Entity
/// Market sınıfı
/// </summary>
public class Market : IMarket
{
    public long Id { get; private set; }
    public string Name { get; private set; }

    // Market'in bağlı olduğu lojistik merkezinin bilgisi
    public ILogisticCenter LogisticsCenter { get; private set; }

    // Market'e ait siparişleri tutar
    public ICollection<Order> Orders { get; private set; } = new List<Order>();

    // Constructor
    public Market(string name, ILogisticCenter logisticsCenter)
    {
        Name = name;
        LogisticsCenter = logisticsCenter;
    }
    public static async Task<IMarket> New(
           string name,
           ILogisticCenter logisticCenter)
    {
        Market market = new(
            name, logisticCenter);
        return market;
    }
   

    //public void PlaceOrder(int productCode, OrderQuantity orderQuantity)
    //{
    //    if (!LogisticsCenter.HasProductInStock(productCode))
    //        throw new InvalidOperationException("Cannot order a product not available in the logistics center.");

    //    var order = new Order(productCode, orderQuantity.Quantity);
    //    _orders.Add(order);
    //}
}
