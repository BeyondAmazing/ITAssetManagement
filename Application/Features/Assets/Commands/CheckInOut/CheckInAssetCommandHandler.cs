using Domain.Interfaces;
using MediatR;

namespace Application.Features.Assets.Commands.CheckInOut;

class CheckInAssetCommandHandler : IRequestHandler<CheckInAssetCommand>
{
    private readonly IAssetRepository _assetRepository;

    public CheckInAssetCommandHandler(IAssetRepository assetService)
    {
        _assetRepository = assetService;
    }

    public async Task Handle(CheckInAssetCommand request, CancellationToken cancellationToken)
    {
        await _assetRepository.CheckInAsync(request.AssetId);
    }
}
