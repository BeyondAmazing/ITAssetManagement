using Domain.Interfaces;
using MediatR;

namespace Application.Features.Assets.Commands.Delete;

public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand>
{
    private readonly IAssetRepository _assetRepository;

    public DeleteAssetCommandHandler(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    async Task IRequestHandler<DeleteAssetCommand>.Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        await _assetRepository.DeleteAsync(request.Id);
    }
}
