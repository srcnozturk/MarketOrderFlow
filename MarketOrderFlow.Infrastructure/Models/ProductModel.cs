namespace MarketOrderFlow.Infrastructure.Models;

public class ProductModel : BaseModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    // Ürün hangi lojistik merkezinde stoklanıyor
    public LogisticsCenterModel LogisticsCenter { get; set; }
}
