using Domain.Entities;
using MediatR;

namespace Application.Features.Manufacturers.Commands.Create;

public record CreateManufacturerCommand(string Name) : IRequest<Manufacturer> { }
