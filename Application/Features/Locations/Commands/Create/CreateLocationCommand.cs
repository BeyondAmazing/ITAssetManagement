using Domain.Entities;
using MediatR;

namespace Application.Features.Locations.Commands.Create;

public record CreateLocationCommand(string name, string? address, string? city, string? state, string? country, Guid? parentId) : IRequest<Location>;