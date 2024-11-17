using MarketOrderFlow.API.Features.Markets.Commands;

namespace MarketOrderFlow.API.Endpoints;

static class MarketEndpoints
{
    public static RouteGroupBuilder MapMarket(this RouteGroupBuilder group)
    {
        group.MapPost("/", AddMarket);
        return group;
    }
    internal async static Task<Results<Created, ProblemHttpResult>> AddMarket(
        [FromBody] CreateMarketCommand cmd, IMediator mediator)
    {
        try
        {
            var handlerResult = await mediator.Send(cmd);
            return TypedResults.Created();
        }
        catch (Exception e)
        {
            ProblemDetails details = new()
            {
                Status = 400,
                Detail = e.Message,
            };
            return TypedResults.Problem(details);
        }
    }
}
