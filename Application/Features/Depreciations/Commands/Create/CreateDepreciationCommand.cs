using Domain.Entities;
using MediatR;

namespace Application.Features.Depreciations.Commands.Create;

public record CreateDepreciationCommand(string Name, int Months) : IRequest<Depreciation> { }
