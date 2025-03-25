using MediatR;

namespace Application.Features.Components.Commands.Assign;

public record AssignComponentCommand(Guid ComponentId, Guid AssetId, int Quantity) : IRequest;
