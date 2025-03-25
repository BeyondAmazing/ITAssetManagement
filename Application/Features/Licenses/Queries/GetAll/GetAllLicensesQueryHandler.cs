using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Licenses.Queries.GetAll;

public class GetAllLicensesQueryHandler : IRequestHandler<GetAllLicensesQuery, IEnumerable<License>>
{
    private readonly ILicenseRepository _licenseRepository;

    public GetAllLicensesQueryHandler(ILicenseRepository licenseRepository)
    {
        _licenseRepository = licenseRepository;
    }

    public async Task<IEnumerable<License>> Handle(GetAllLicensesQuery request, CancellationToken cancellationToken)
    {
        return await _licenseRepository.GetAllAsync();
    }
}
