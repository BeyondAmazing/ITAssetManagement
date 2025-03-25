using MediatR;

namespace Application.Features.Assets.Commands.CheckInOut;

public record CheckInAssetCommand(Guid AssetId): IRequest;
