using MarketOrderFlow.Application.Commands;
using MarketOrderFlow.Application.Concracts;

namespace MarketOrderFlow.API.Endpoints;

static class OrderEndpoints
{
    public static RouteGroupBuilder MapOrder(this RouteGroupBuilder group)
    {
        group.MapPost("/", OrderDaily).RequireAuthorization("Manager");
        group.MapPost("/approveOrder", ApproveOrder).RequireAuthorization("Manager");
        group.MapDelete("/{GlobalId}", RemoveOrder).RequireAuthorization("Manager"); 
        return group;
    }
    internal static async Task<Results<Created, ProblemHttpResult>> OrderDaily(IOrderService orderService)
    {
        try
        {
            var result = await orderService.GenerateDailyOrders();
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
    internal static async Task<Results<Ok, ProblemHttpResult>> ApproveOrder
        ([FromServices] IOrderService orderService,
        ApproveOrderCommand cmd)
    {
        try
        {
            var result = await orderService.ApproveOrderAsync(cmd);
            return TypedResults.Ok();
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
    internal static async Task<Results<Ok, ProblemHttpResult>> RemoveOrder
        ([FromServices] IOrderService orderService,
        [FromRoute] Guid GlobalId)
    {
        try
        {
            var cmd = new RemoveOrderCommand(GlobalId);
            var result = await orderService.RemoveProductFromOrderAsync(cmd);
            return TypedResults.Ok();
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
