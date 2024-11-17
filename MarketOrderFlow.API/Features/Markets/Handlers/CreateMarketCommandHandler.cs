using Mapster;
using MarketOrderFlow.API.Features.Markets.Commands;
using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.API.Features.Markets.Handlers;

public class CreateMarketCommandHandler(ApplicationDbContext db) : IRequestHandler<CreateMarketCommand, Result>
{
    public async Task<Result> Handle(CreateMarketCommand request, CancellationToken cancellationToken)
    {
        var logisticCenterModel = await db.LogisticsCenters.FirstOrDefaultAsync(lc => lc.GlobalId == request.LogisticCenterCommand.GlobalId);
        if (logisticCenterModel is null) return null;
        var logisticCenterDomain = logisticCenterModel.Adapt<ILogisticCenter>();
        
        var createMarket = await Market.New(request.Name, logisticCenterDomain);
        var createMarketModel= createMarket.Adapt<MarketModel>();
        createMarketModel.LogisticsCenter = logisticCenterModel;

        await db.Markets.AddAsync(createMarketModel);
        return await db.SaveEntitiesAsync(cancellationToken);
    }
}
