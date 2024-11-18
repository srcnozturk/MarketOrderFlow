using MarketOrderFlow.Infrastructure;
using MediatR;

namespace MarketOrderFlow.Application.Commands;

public readonly record struct RemoveOrderCommand(Guid? OrderGlobalId) : IRequest<Result>;
