using Domain.Entities;
using MediatR;

namespace Application.Features.Depreciations.Queries.GetById;

public record GetDepreciationByIdQuery(Guid Id) : IRequest<Depreciation?> { }
