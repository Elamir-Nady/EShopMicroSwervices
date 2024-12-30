namespace Basket.Api.Basket.GetBasket;

public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);
public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
{
    public GetBasketQueryValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName Is Required");
    }
}
internal class GetBasketQueryHandler (IBasketRepostory _repostory)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var Basket =await _repostory.GetBasket(query.UserName,cancellationToken);
        return new GetBasketResult(Basket);
    }
}
