using MarketOrderFlow.API.Features.LogisticCenter.Commands;

namespace MarketOrderFlow.API.Features.Markets.Commands;

public readonly record struct CreateMarketCommand(string Name,CreateLogisticCenterCommand LogisticCenterCommand):IRequest<Result>;