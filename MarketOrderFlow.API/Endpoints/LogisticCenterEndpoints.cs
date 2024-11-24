using MarketOrderFlow.API.Features.LogisticCenter.Commands;
using MarketOrderFlow.API.Features.LogisticCenter.Queries;

namespace MarketOrderFlow.API.Endpoints;

static class LogisticCenterEndpoints
{
    public static RouteGroupBuilder MapLogisticCenter(this RouteGroupBuilder group)
    {
        group.MapPost("/", AddLogisticCenter).RequireAuthorization("Admin");
        group.MapGet("/", ListLogisticCenter)/*.RequireAuthorization("AllRoles")*/;
        return group;
    }
    internal async static Task<Results<Created, ProblemHttpResult>> AddLogisticCenter(
        [FromBody] CreateLogisticCenterCommand cmd, IMediator mediator)
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
    internal static async Task<IResult> ListLogisticCenter(
      IMediator mediator)
    {
        try
        {
            var handlerResult = await mediator.Send(new ListLogisticCenterQuery());
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
