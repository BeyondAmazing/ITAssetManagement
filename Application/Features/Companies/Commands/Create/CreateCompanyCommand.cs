using Domain.Entities;
using MediatR;

namespace Application.Features.Companies.Commands.Create;

public record CreateCompanyCommand(string name, string? email, string? phone) : IRequest<Company> { }
