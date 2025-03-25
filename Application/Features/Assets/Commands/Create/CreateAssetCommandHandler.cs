using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Assets.Commands.Create;

public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Asset>
{
    private readonly IAssetRepository _assetRepository;
    private readonly IMapper _mapper;

    public CreateAssetCommandHandler(IAssetRepository assetRepository, IMapper mapper)
    {
        _assetRepository = assetRepository;
        _mapper = mapper;
    }

    public async Task<Asset> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = _mapper.Map<Asset>(request);
        return await _assetRepository.AddAsync(asset);
    }
}
