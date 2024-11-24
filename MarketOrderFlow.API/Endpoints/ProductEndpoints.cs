using MarketOrderFlow.API.Features.Products.Commands;
using MarketOrderFlow.API.Features.Products.Queries;

namespace MarketOrderFlow.API.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProduct(this RouteGroupBuilder group)
    {
        group.MapPost("/", AddProduct).RequireAuthorization("ManagerOrAdmin");
        group.MapGet("/", ListProducts).RequireAuthorization("AllRoles");
        return group;
    }
    public async static Task<Results<Created, ProblemHttpResult>> AddProduct(
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
    public static async Task<IResult> ListProducts(
     IMediator mediator)
    {
        try
        {
            var handlerResult = await mediator.Send(new ListProductsQuery());
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
