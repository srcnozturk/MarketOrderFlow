using MarketOrderFlow.API.Features.Markets.Commands;
using MarketOrderFlow.API.Features.Markets.Handlers;
using MarketOrderFlow.API.Features.Markets.Queries;

namespace MarketOrderFlow.API.Endpoints;

static class MarketEndpoints
{
    public static RouteGroupBuilder MapMarket(this RouteGroupBuilder group)
    {
        group.MapPost("/", AddMarket);
        group.MapPost("/addProductToMarket", AddProductToMarket);
        group.MapGet("/", ListMarkets);
        group.MapGet("/listProductToMarkets", ListProductToMarkets);
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
    internal static async Task<IResult> ListMarkets(
      IMediator mediator)
    {
        try
        {
            var handlerResult = await mediator.Send(new ListMarketsQuery());
            return TypedResults.Ok(handlerResult);
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
    internal async static Task<Results<Created, ProblemHttpResult>> AddProductToMarket(
        [FromBody] AddProductToMarketCommand cmd, IMediator mediator)
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
    internal static async Task<IResult> ListProductToMarkets(
      IMediator mediator)
    {
        try
        {
            var handlerResult = await mediator.Send(new ListProductStockToMarketQuery());
            return TypedResults.Ok(handlerResult);
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
