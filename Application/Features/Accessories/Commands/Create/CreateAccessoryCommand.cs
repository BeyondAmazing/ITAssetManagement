using Domain.Entities;
using MediatR;

namespace Application.Features.Accessories.Commands.Create;

public record CreateAccessoryCommand(
string Name,
Guid CategoryId,
Guid? CompanyId,
Guid? ManufacturerId,
int Quantity,
DateTime? purchaseDate, 
decimal purchaseCost) : IRequest<Accessory>;
