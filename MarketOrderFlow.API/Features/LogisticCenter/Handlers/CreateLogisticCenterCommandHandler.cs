using Mapster;

namespace MarketOrderFlow.API.Features.LogisticCenter.Commands;

public class CreateLogisticCenterCommandHandler(ApplicationDbContext db) 
    : IRequestHandler<CreateLogisticCenterCommand, Result>
{
    public async Task<Result> Handle(CreateLogisticCenterCommand request, CancellationToken cancellationToken)
    {
        var createLogisticCenter = await LogisticsCenter.Create(request.Name);
        var logisticCenterModel=createLogisticCenter.Adapt<LogisticsCenterModel>();
        if (logisticCenterModel is null) return null;

        await db.LogisticsCenters.AddAsync(logisticCenterModel);
        return await db.SaveEntitiesAsync(cancellationToken);
    }
}
