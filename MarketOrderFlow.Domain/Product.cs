namespace MarketOrderFlow.Domain;

/// <summary>
/// Entity
/// Ürün 
/// </summary>
public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    // Ürün hangi lojistik merkezinde stoklanıyor
    public int LogisticsCenterId { get; private set; }
    public LogisticsCenter LogisticsCenter { get; private set; }

    // Constructor
    public Product(string name, decimal price, int logisticsCenterId)
    {
        Name = name;
        Price = price;
        LogisticsCenterId = logisticsCenterId;
    }
}
