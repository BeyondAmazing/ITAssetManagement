using Domain.Entities;
using MediatR;

namespace Application.Features.Licenses.Queries.GetAll;

public record GetAllLicensesQuery : IRequest<IEnumerable<License>>{}
