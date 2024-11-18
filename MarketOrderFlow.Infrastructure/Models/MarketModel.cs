namespace MarketOrderFlow.Infrastructure.Models;

public class MarketModel : BaseModel
{
    public string Name { get; set; }
    public LogisticsCenterModel LogisticsCenter { get; set; }
    public List<ProductModel>? Products { get; set; }
}
