namespace MarketOrderFlow.Infrastructure.Models;

public class MarketProductStockModel :BaseModel
{
    public MarketModel Market { get; set; }
    public ProductModel  Product { get; set; }
    public int StockQuantity { get; set; }
}
