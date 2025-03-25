using MediatR;

namespace Application.Features.Consumables.Commands.CheckOut;

public record CheckOutConsumableCommand(Guid ConsumableId, Guid UserId, int Quantity) : IRequest { }
