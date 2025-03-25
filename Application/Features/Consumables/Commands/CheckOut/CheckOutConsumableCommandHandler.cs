using Domain.Interfaces;
using MediatR;

namespace Application.Features.Consumables.Commands.CheckOut;
public class CheckOutConsumableCommandHandler : IRequestHandler<CheckOutConsumableCommand>
{
    private readonly IConsumableRepository _consumableRepository;

    public CheckOutConsumableCommandHandler(IConsumableRepository consumableRepository)
    {
        _consumableRepository = consumableRepository;
    }

    public async Task Handle(CheckOutConsumableCommand request, CancellationToken cancellationToken)
    {
        await _consumableRepository.CheckOutAsync(request.ConsumableId, request.UserId, request.Quantity);
    }
}
