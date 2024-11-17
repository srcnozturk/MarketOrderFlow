using MarketOrderFlow.API.Features.LogisticCenter.Commands;

namespace MarketOrderFlow.API.Features.Products.Commands;

public readonly record struct CreateProductCommand(string Name,int Quantity,int Barcode,CreateLogisticCenterCommand CreateLogisticCenter): IRequest<Result>;


