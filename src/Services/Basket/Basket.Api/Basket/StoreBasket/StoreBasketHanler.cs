

namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x=>x.Cart).NotEmpty().WithMessage("Cart Can Not Be Null");
            RuleFor(x=>x.Cart.UserName).NotEmpty().WithMessage("UserName IS Required");
        }
    }
    internal class StoreBasketCommandHanler(IBasketRepostory _repostory)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
             
            var Basket = await _repostory.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(Basket.UserName);
        }
    }
}
