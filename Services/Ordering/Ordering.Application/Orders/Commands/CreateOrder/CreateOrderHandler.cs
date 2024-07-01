
namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler :
    ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        //Create Order entity from command Object
        //Save to db
        //return result

        throw new NotImplementedException();
    }
}
