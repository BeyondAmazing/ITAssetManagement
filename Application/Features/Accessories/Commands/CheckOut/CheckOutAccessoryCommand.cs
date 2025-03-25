using MediatR;

namespace Application.Features.Accessories.Commands.CheckOut;

public record CheckOutAccessoryCommand(Guid AccessoryId, Guid UserId, int Quantity) : IRequest;
