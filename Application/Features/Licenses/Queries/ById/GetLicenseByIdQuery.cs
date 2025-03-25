using Domain.Entities;
using MediatR;

namespace Application.Features.Licenses.Queries.ById;

public record GetLicenseByIdQuery(Guid Id) : IRequest<License?>;