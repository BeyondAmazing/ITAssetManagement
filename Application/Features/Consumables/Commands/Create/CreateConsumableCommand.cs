using Domain.Entities;
using MediatR;

namespace Application.Features.Consumables.Commands.Create;

public record CreateConsumableCommand(string Name, Guid CategoryId, Guid? CompanyId, int Quantity, decimal purchaseCost) : IRequest<Consumable> { }
