using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.Domain;

/// <summary>
/// Entity
/// Ürün 
/// </summary>
public class Product : IProduct
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public int StockQuantity { get; private set; }
    public int Barcode { get; private set; }
    // Ürün hangi lojistik merkezinde stoklanıyor
    public ILogisticCenter LogisticCenter { get; }

    // Constructor
    public Product(string name, int stockQuantity,int barcode,ILogisticCenter logisticCenter)
    {
        Name = name;
        StockQuantity = stockQuantity;
        Barcode=barcode;
        LogisticCenter = logisticCenter;
    }
    public static async Task<IProduct> New(
           string name,
           int stockQuantity,
           int barcode,
           ILogisticCenter logisticCenter)
    {
        Product product = new(
            name, stockQuantity, barcode, logisticCenter);
        return product;
    }
}
