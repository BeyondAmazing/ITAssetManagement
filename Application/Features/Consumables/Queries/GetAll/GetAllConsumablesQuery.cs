using Domain.Entities;
using MediatR;

namespace Application.Features.Consumables.Queries.GetAll;

public record GetAllConsumablesQuery : IRequest<IEnumerable<Consumable>> { }
