using Domain.Entities;
using MediatR;

namespace Application.Features.StatusLabels.Commands.Create;

public record CreateStatusLabelCommand(string Name, bool? Deployable, bool? Pending, bool? Archived) : IRequest<StatusLabel> { }
