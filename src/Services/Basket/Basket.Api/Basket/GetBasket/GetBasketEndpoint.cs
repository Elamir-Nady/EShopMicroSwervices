using Basket.Api.Models;

namespace Basket.Api.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);
public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapGet("/Basket/{UserName}", async (string UserName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(UserName));
            var response= result.Adapt<GetBasketResponse>();
            return Results.Ok(response);

        })
            .WithName("GetBasket")
            .WithDescription("Get Basket")
            .WithSummary("Get Basket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
