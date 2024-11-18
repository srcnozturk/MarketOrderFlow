namespace MarketOrderFlow.API.Features.Markets.Commands;

public readonly record struct AddProductToMarketCommand(FindProductCommand Product, FindMarketCommand Market,int StockQuantity) : IRequest<Result>;

public readonly record struct FindProductCommand(Guid GlobalId):IRequest<Result>;
public readonly record struct FindMarketCommand(Guid GlobalId):IRequest<Result>;

