using Domain.Entities;
using MediatR;

namespace Application.Features.Companies.Queries.GetAll;

public record GetAllCompaniesQuery : IRequest<IEnumerable<Company>>;
