using Domain.Entities;
using MediatR;

namespace Application.Features.StatusLabels.Queries.GetById;

public record GetStatusLabelByIdQuery(Guid Id) : IRequest<StatusLabel?> { }
