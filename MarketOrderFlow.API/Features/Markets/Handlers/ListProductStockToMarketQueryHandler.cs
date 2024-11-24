using MarketOrderFlow.API.Features.Markets.Queries;

namespace MarketOrderFlow.API.Features.Markets.Handlers;

public class ListProductStockToMarketQueryHandler(ApplicationDbContext db) 
    : IRequestHandler<ListProductStockToMarketQuery, MarketProductStockModel[]>
{
    public async Task<MarketProductStockModel[]> Handle(ListProductStockToMarketQuery request, CancellationToken cancellationToken)
    {
        var marketProductStockModelArray= await db.MarketProductStocks
            .Include(p=> p.Product)
            .Include(m=> m.Market)
            .Where(mp=> mp.IsDeleted!=true)
            .ToArrayAsync();
        if (!marketProductStockModelArray.Any()) return [];

        return marketProductStockModelArray;
    }
}
