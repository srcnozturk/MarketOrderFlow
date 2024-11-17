using MarketOrderFlow.Application.Concracts;

namespace MarketOrderFlow.API.Endpoints;

static class OrderEndpoints
{
    public static RouteGroupBuilder MapOrder(this RouteGroupBuilder group)
    {
        group.MapPost("/", OrderDaily);
        return group;
    }
    internal static async Task<Results<Created, ProblemHttpResult>> OrderDaily(IOrderService orderService)
    {
        try
        {
            var result = await orderService.GenerateDailyOrders();

            if (result.IsSuccess)
                return TypedResults.Created();

            return TypedResults.Problem("Failed to generate daily orders.");
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
