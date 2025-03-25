using Domain.Interfaces;
using MediatR;

namespace Application.Features.Accessories.Commands.CheckOut;

public class CheckOutAccessoryCommandHandler : IRequestHandler<CheckOutAccessoryCommand>
{
    private readonly IAccessoryRepository _accessoryRepository;

    public CheckOutAccessoryCommandHandler(IAccessoryRepository accessoryRepository)
    {
        _accessoryRepository = accessoryRepository;
    }

    public async Task Handle(CheckOutAccessoryCommand request, CancellationToken cancellationToken)
    {
        await _accessoryRepository.CheckOutAsync(request.AccessoryId, request.UserId, request.Quantity);
    }
}
