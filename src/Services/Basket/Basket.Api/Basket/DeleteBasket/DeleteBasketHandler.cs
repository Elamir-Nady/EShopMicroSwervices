
using Basket.Api.Data;

namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBasketCmmand(string UserName):ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);
public class DeleteBasketCmmandValidator : AbstractValidator<DeleteBasketCmmand>
{
    public DeleteBasketCmmandValidator()
    {
        RuleFor(x=>x.UserName).NotEmpty().WithMessage("UserName Is Required");
    }
}
internal class DeleteBasketCommandHandler (IBasketRepostory _repostory)
    : ICommandHandler<DeleteBasketCmmand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCmmand command, CancellationToken cancellationToken)
    {
       var result= await _repostory.DeleteBasket(command.UserName);
        return new DeleteBasketResult(result);
        
    }
}
