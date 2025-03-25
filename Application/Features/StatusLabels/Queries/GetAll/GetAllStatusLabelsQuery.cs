using Domain.Entities;
using MediatR;

namespace Application.Features.StatusLabels.Queries.GetAll;

public record GetAllStatusLabelsQuery : IRequest<IEnumerable<StatusLabel>> { }
