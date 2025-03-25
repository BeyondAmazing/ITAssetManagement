using Domain.Entities;
using MediatR;

namespace Application.Features.Depreciations.Queries.GetAll;

public record GetAllDepreciationsQuery : IRequest<IEnumerable<Depreciation>> { }
