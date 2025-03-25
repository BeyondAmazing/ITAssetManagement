using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Licenses.Queries.ById;

public class GetLicenseByIdQueryHandler : IRequestHandler<GetLicenseByIdQuery, License?>
{
    private readonly ILicenseRepository _licenseRepository;

    public GetLicenseByIdQueryHandler(ILicenseRepository licenseRepository)
    {
        _licenseRepository = licenseRepository;
    }

    public async Task<License?> Handle(GetLicenseByIdQuery request, CancellationToken cancellationToken)
    {
        return await _licenseRepository.GetByIdAsync(request.Id);
    }
}
