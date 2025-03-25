using MediatR;

namespace Application.Features.Assets.Commands.Delete;

public record DeleteAssetCommand(Guid Id): IRequest;
