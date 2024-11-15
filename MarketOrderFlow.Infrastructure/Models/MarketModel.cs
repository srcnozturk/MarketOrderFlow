namespace MarketOrderFlow.Infrastructure.Models;

public class MarketModel : BaseModel
{
    public string Name { get; set; }

    // Market'in bağlı olduğu lojistik merkezinin bilgisi
    public LogisticsCenterModel LogisticsCenter { get; set; }
}
