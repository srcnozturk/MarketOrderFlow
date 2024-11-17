using MarketOrderFlow.API.Features.LogisticCenter.Queries;

namespace MarketOrderFlow.API.Features.LogisticCenter.Handlers;

public class ListLogisticCenterQueryHandler(ApplicationDbContext db) 
    : IRequestHandler<ListLogisticCenterQuery, LogisticsCenterModel[]>
{
    public async Task<LogisticsCenterModel[]> Handle(ListLogisticCenterQuery request, CancellationToken cancellationToken)
    {
        return await db.LogisticsCenters.ToArrayAsync();
    }
}
