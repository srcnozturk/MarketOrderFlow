using MarketOrderFlow.API.Features.Products.Commands;

namespace MarketOrderFlow.API.Endpoints;

static class ProductEndpoints
{
    public static RouteGroupBuilder MapProduct(this RouteGroupBuilder group)
    {
        group.MapPost("/", AddProduct);
        return group;
    }
    internal async static Task<Results<Created, ProblemHttpResult>> AddProduct(
        [FromBody] CreateProductCommand cmd, IMediator mediator)
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
