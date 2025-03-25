using Domain.Entities;
using MediatR;

namespace Application.Features.Companies.Queries.GetById;

public record GetCompanyByIdQuery(Guid Id) : IRequest<Company?> { }
