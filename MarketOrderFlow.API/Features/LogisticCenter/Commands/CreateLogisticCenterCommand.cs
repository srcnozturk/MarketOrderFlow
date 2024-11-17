namespace MarketOrderFlow.API.Features.LogisticCenter.Commands;

public  readonly record struct  CreateLogisticCenterCommand(string Name,Guid? GlobalId) : IRequest<Result>;
