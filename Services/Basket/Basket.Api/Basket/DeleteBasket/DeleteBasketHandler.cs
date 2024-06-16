namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);
public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is Required");
    }
}
public class DeleteBasketCommandHandler(IMediator mediator)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        string EventMessage = string.Empty;
        await _mediator.Publish(new BasketDeletedDomainEvent(request.UserName, EventMessage), cancellationToken);
        return new DeleteBasketResult(true);
    }
}
