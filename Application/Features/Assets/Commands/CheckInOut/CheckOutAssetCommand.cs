using MediatR;

namespace Application.Features.Assets.Commands.CheckInOut;

public record CheckOutAssetCommand(Guid AssetId, Guid UserId): IRequest;