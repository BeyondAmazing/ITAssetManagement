using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.StatusLabels.Commands.Create;
public class CreateStatusLabelCommandHandler : IRequestHandler<CreateStatusLabelCommand, StatusLabel>
{
    private readonly IStatusLabelRepository _statusLabelRepository;

    public CreateStatusLabelCommandHandler(IStatusLabelRepository statusLabelRepository)
    {
        _statusLabelRepository = statusLabelRepository;
    }

    public async Task<StatusLabel> Handle(CreateStatusLabelCommand request, CancellationToken cancellationToken)
    {
        var statusLabel = StatusLabel.Create(request.Name, request.Deployable, request.Pending, request.Archived);
        return await _statusLabelRepository.AddAsync(statusLabel);
    }
}
