using Domain.Entities;
using MediatR;

namespace Application.Features.Components.Commands.Create;

public record CreateComponentCommand(string Name, Guid CategoryId, Guid? CompanyId, string serial, int Quantity, decimal purchaseCost) : IRequest<Component> { }
