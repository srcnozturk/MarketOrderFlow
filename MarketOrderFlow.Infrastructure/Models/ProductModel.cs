namespace MarketOrderFlow.Infrastructure.Models;

public class ProductModel : BaseModel
{
    public string Name { get; set; }
    public int StockQuantity { get; set; }
    public LogisticsCenterModel LogisticsCenter { get; set; }
    public List<OrderModel>? Orders { get; set; } = [];
    public List<MarketModel>? Markets { get; set; }
}
