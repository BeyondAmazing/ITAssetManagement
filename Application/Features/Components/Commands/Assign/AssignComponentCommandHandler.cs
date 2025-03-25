using Domain.Interfaces;
using MediatR;

namespace Application.Features.Components.Commands.Assign;
public class AssignComponentCommandHandler : IRequestHandler<AssignComponentCommand>
{
    private readonly IComponentRepository _componentRepository;

    public AssignComponentCommandHandler(IComponentRepository componentRepository)
    {
        _componentRepository = componentRepository;
    }

    public async Task Handle(AssignComponentCommand request, CancellationToken cancellationToken)
    {
        await _componentRepository.AssignToAssetAsync(request.ComponentId, request.AssetId, request.Quantity);
    }
}
