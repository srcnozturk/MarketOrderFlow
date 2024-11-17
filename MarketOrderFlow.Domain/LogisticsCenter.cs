using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.Domain;

/// <summary>
/// Entity
/// Lojistik merkezi
/// </summary>
public class LogisticsCenter : ILogisticCenter
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    // Lojistik merkezinin stokladığı ürünleri tutar
    public ICollection<Product> Products { get; private set; } = new List<Product>();

    // Constructor
    public LogisticsCenter(string name)
    {
        Name = name;
    }
    public static async Task<ILogisticCenter> Create(string name)
    {
        return new LogisticsCenter(name);
    }
    //public bool HasProductInStock(int productCode) => _productCodesInStock.Contains(productCode);

}
