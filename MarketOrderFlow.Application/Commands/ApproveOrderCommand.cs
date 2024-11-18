using MarketOrderFlow.Infrastructure;
using MediatR;

namespace MarketOrderFlow.Application.Commands;

public readonly record struct ApproveOrderCommand(Guid OrderGlobalId,Guid MarketGlobalId,int ApprovedQuantity) : IRequest<Result>;

