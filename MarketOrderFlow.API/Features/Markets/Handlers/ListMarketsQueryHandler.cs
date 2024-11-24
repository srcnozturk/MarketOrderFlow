using MarketOrderFlow.API.Features.Markets.Queries;

namespace MarketOrderFlow.API.Features.Markets.Handlers;

public class ListMarketsQueryHandler(ApplicationDbContext db)
    : IRequestHandler<ListMarketsQuery, MarketModel[]>
{
    public async Task<MarketModel[]> Handle(ListMarketsQuery query, CancellationToken cancellationToken)
    {
        var marketModelArray= await db.Markets.Where(mp => mp.IsDeleted != true).ToArrayAsync();
        if (!marketModelArray.Any()) return [];

        return marketModelArray;
    }
}
